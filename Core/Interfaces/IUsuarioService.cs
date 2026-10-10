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

        Task<List<UsuarioListagemDTO>> ListarAsync(string? filtro,bool inativo = false);

        Task<UsuarioCompletoDTO?> ConsultarPorIdAsync(Guid usuarioId,bool inativo = false);

        Task AtualizarAsync(UsuarioCompletoDTO dto,string? novaSenha = null);

        Task AlterarStatusAsync(Guid usuarioId,bool inativo);

    }
}
