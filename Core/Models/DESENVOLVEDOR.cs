using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class DESENVOLVEDOR
    {
        public int Id { get; set; }

        public string? Nome { get; set; }

        public virtual ICollection<TAREFA> Desenvolvedor { get; set; } =
            new List<TAREFA>();
    }

    public static class LISTADESENVOLVEDOR
    {
        public static List<DESENVOLVEDOR> ObterDesenvolvedor()
        {
            return new List<DESENVOLVEDOR>
        {
            new DESENVOLVEDOR { Id = 1, Nome = "Evandro" }
        };
        }
    }
}
