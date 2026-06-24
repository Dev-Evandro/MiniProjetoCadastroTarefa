using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.Usuario
{
    public class UsuarioCompletoDTO
    {
        public USUARIO Usuario { get; set; } = new();

        public PermissoesUsuario Permissoes { get; set; } = new();
    }
}
