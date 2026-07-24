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

            if (string.IsNullOrWhiteSpace(dto.Usuario.Nome))
                throw new ArgumentException("Informe o nome do usuário.");

            if (string.IsNullOrWhiteSpace(dto.Usuario.Login))
                throw new ArgumentException("Informe o login do usuário.");

            if (string.IsNullOrWhiteSpace(dto.Usuario.SenhaHash))
                throw new ArgumentException("Informe a senha do usuário.");

            dto.Usuario.UsuarioId = Guid.NewGuid();
            dto.Usuario.DataCadastro = DateTime.Now;
            dto.Usuario.DataAtualizacao = null;
            dto.Usuario.Inativo = false;

            if (await _usuarioDAL.LoginExisteAsync(dto.Usuario.Login))
                throw new InvalidOperationException("Este login já está cadastrado.");

            dto.Usuario.SenhaHash = _senhaService.GerarHash(dto.Usuario.SenhaHash);

            string permissoesJson = JsonSerializer.Serialize(dto.Permissoes);

            await _usuarioDAL.InserirAsync(dto.Usuario,permissoesJson);
        }

        
        #region ImplementarDepois
        public async Task AtualizarAsync(USUARIO usuario,PermissoesUsuario permissoes)
        {

        }

        public async Task<UsuarioCompletoDTO?> ObterPorIdAsync(Guid usuarioId)
        {
            return null;
        }

        public async Task<IEnumerable<USUARIO>> ListarAsync()
        {
            return Enumerable.Empty<USUARIO>();
        }

        public async Task ExcluirAsync(Guid usuarioId)
        {

        }

        #endregion
    }
}
