using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class PermissaoTela
    {
        public bool Visualizar { get; set; }

        public bool Cadastrar { get; set; }

        public bool Editar { get; set; }

        public bool Excluir { get; set; }
    }
}
