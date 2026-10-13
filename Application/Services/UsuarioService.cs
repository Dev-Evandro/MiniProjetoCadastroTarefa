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

        public UsuarioService(IUsuarioDAL usuarioDAL,ISenhaService senhaService)
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

        public async Task<List<UsuarioListagemDTO>> ListarAsync(string? filtro,bool inativo = false)
        {
            var usuarios = await _usuarioDAL.ListarAsync(filtro, inativo);

            return usuarios
                .Select(usuario => new UsuarioListagemDTO
                {
                    UsuarioId = usuario.UsuarioId,
                    Nome = usuario.Nome,
                    Login = usuario.Login,
                    DataCadastro = usuario.DataCadastro,
                    DataAtualizacao = usuario.DataAtualizacao,
                    Inativo = usuario.Inativo
                })
                .ToList();
        }
        public async Task<UsuarioCompletoDTO?> ConsultarPorIdAsync(Guid usuarioId,bool inativo = false)
        {
            var registro = await _usuarioDAL.ConsultarPorIdAsync(
                usuarioId,
                inativo);

            if (registro is null)
                return null;

            var permissoes = JsonSerializer.Deserialize<PermissoesUsuario>(
                registro.Value.PermissoesJson);

            return new UsuarioCompletoDTO
            {
                Usuario = registro.Value.Usuario,
                Permissoes = permissoes ?? new PermissoesUsuario()
            };
        }

        public async Task AtualizarAsync(UsuarioCompletoDTO dto,string? novaSenha = null)
        {
            ArgumentNullException.ThrowIfNull(dto);
            ArgumentNullException.ThrowIfNull(dto.Usuario);
            ArgumentNullException.ThrowIfNull(dto.Permissoes);

            dto.Usuario.Nome = dto.Usuario.Nome.Trim();
            dto.Usuario.Login = dto.Usuario.Login.Trim();

            if (string.IsNullOrWhiteSpace(dto.Usuario.Nome))
                throw new ArgumentException("Informe o nome do usuário.");

            if (string.IsNullOrWhiteSpace(dto.Usuario.Login))
                throw new ArgumentException("Informe o login do usuário.");

            if (dto.Usuario.Nome.Length > 100)
                throw new ArgumentException(
                    "O nome deve possuir no máximo 100 caracteres.");

            if (dto.Usuario.Login.Length > 50)
                throw new ArgumentException(
                    "O login deve possuir no máximo 50 caracteres.");

            if (dto.Usuario.UsuarioId == Guid.Empty)
                throw new ArgumentException("O usuário informado é inválido.");

            if (await _usuarioDAL.LoginExisteAsync(dto.Usuario.Login,dto.Usuario.UsuarioId))
            {
                throw new InvalidOperationException(
                    "Este login já está cadastrado.");
            }

            var atualizarSenha = !string.IsNullOrWhiteSpace(novaSenha);

            if (atualizarSenha)
            {
                if (novaSenha!.Length < 6)
                    throw new ArgumentException(
                        "A senha deve possuir pelo menos 6 caracteres.");

                dto.Usuario.SenhaHash = _senhaService.GerarHash(novaSenha);
            }

            dto.Usuario.DataAtualizacao = DateTime.Now;

            NormalizarPermissoes(dto.Permissoes);

            var permissoesJson = JsonSerializer.Serialize(dto.Permissoes);

            await _usuarioDAL.AtualizarAsync(
                dto.Usuario,
                permissoesJson,
                atualizarSenha);
        }

        public async Task AlterarStatusAsync(Guid usuarioId, bool inativo)
        {
            if (usuarioId == Guid.Empty)
                throw new ArgumentException("O usuário informado é inválido.");

            await _usuarioDAL.AlterarStatusAsync(usuarioId, inativo);
        }

    }
}