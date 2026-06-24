using Core.DTOs.Usuario;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IUsuarioDAL
    {
        Task InserirAsync(
            USUARIO usuario,
            string permissoesJson);

        Task AtualizarAsync(
            USUARIO usuario,
            string permissoesJson);

        Task<UsuarioCompletoDTO?> ObterPorIdAsync(
            Guid usuarioId);

        Task<IEnumerable<USUARIO>> ListarAsync();

        Task ExcluirAsync(Guid usuarioId);
    }
}
