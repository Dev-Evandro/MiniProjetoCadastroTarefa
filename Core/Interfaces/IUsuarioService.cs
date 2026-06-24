using Core.DTOs.Usuario;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IUsuarioService
    {
        Task InserirAsync(UsuarioCompletoDTO dto);

        Task AtualizarAsync(
            USUARIO usuario,
            PermissoesUsuario permissoes);
        

        Task<UsuarioCompletoDTO?> ObterPorIdAsync(Guid usuarioId);

        Task<IEnumerable<USUARIO>> ListarAsync();

        Task ExcluirAsync(Guid usuarioId);
    }
}
