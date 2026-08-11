using Core.DTOs.Usuario;
using Core.Interfaces;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioDAL _usuarioDAL;
        private readonly ISenhaService _senhaService;

        public UsuarioService(
            IUsuarioDAL usuarioDAL,
            ISenhaService senhaService)
        {
            _usuarioDAL = usuarioDAL;
            _senhaService = senhaService;
        }

        public async Task InserirAsync(UsuarioCompletoDTO dto)
        {
            ArgumentNullException.ThrowIfNull(dto);

            dto.Usuario.Nome = dto.Usuario.Nome.Trim();
            dto.Usuario.Login = dto.Usuario.Login.Trim();

            if (string.IsNullOrWhiteSpace(dto.Usuario.Nome))
                throw new ArgumentException("Informe o nome do usuário.");

            if (string.IsNullOrWhiteSpace(dto.Usuario.Login))
                throw new ArgumentException("Informe o login do usuário.");

            if (string.IsNullOrWhiteSpace(dto.Usuario.SenhaHash))
                throw new ArgumentException("Informe a senha do usuário.");

            if (dto.Usuario.Nome.Length > 100)
                throw new ArgumentException(
                    "O nome deve possuir no máximo 100 caracteres.");

            if (dto.Usuario.Login.Length > 50)
                throw new ArgumentException(
                    "O login deve possuir no máximo 50 caracteres.");

            if (dto.Usuario.SenhaHash.Length < 6)
                throw new ArgumentException(
                    "A senha deve possuir pelo menos 6 caracteres.");

            if (await _usuarioDAL.LoginExisteAsync(dto.Usuario.Login))
            {
                throw new InvalidOperationException(
                    "Este login já está cadastrado.");
            }

            dto.Usuario.UsuarioId = Guid.NewGuid();
            dto.Usuario.DataCadastro = DateTime.Now;
            dto.Usuario.DataAtualizacao = null;
            dto.Usuario.Inativo = false;

            dto.Usuario.SenhaHash =
                _senhaService.GerarHash(dto.Usuario.SenhaHash);

            NormalizarPermissoes(dto.Permissoes);

            string permissoesJson =
                JsonSerializer.Serialize(dto.Permissoes);

            await _usuarioDAL.InserirAsync(
                dto.Usuario,
                permissoesJson);
        }

        private static void NormalizarPermissoes(
            PermissoesUsuario permissoes)
        {
            foreach (var permissao in permissoes.Telas.Values)
            {
                if (permissao.Visualizar)
                    continue;

                permissao.Cadastrar = false;
                permissao.Editar = false;
                permissao.Excluir = false;
            }
        }
    }
}