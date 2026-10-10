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
        Task InserirAsync(USUARIO usuario,string permissoesJson);

        Task<bool> LoginExisteAsync(string login,Guid? ignorarUsuarioId = null);

        Task<List<USUARIO>> ListarAsync(string? filtro,bool inativo = false);

        Task<(USUARIO Usuario, string PermissoesJson)?> ConsultarPorIdAsync(
            Guid usuarioId,
            bool inativo = false);

        Task AtualizarAsync(USUARIO usuario,string permissoesJson,bool atualizarSenha);

        Task AlterarStatusAsync(Guid usuarioId, bool inativo);


    }
}
