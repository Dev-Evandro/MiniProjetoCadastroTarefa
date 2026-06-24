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

        public UsuarioService(IUsuarioDAL usuarioDAL)
        {
            _usuarioDAL = usuarioDAL;
        }

        public async Task InserirAsync(USUARIO usuario,PermissoesUsuario permissoes)
        {
            if (string.IsNullOrWhiteSpace(usuario.Nome))
                throw new Exception("Informe o nome.");

            if (string.IsNullOrWhiteSpace(usuario.Login))
                throw new Exception("Informe o login.");

            usuario.UsuarioId = Guid.NewGuid();
            usuario.DataCadastro = DateTime.Now;
            usuario.Inativo = false;

            string permissoesJson =
                JsonSerializer.Serialize(permissoes);

            await _usuarioDAL.InserirAsync(
                usuario,
                permissoesJson);
        }

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

        public Task InserirAsync(UsuarioCompletoDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
