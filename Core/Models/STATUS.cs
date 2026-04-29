using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class STATUS
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public virtual ICollection<TAREFA> Tarefas { get; set; } = new List<TAREFA>();
    }

    public static class STATUSHELPER
    {
        public static List<STATUS> ObterStatus()
        {
            return new List<STATUS>
        {
            new STATUS { Id = 1, Nome = "Pendente" },
            new STATUS { Id = 2, Nome = "Em Andamento" },
            new STATUS { Id = 3, Nome = "Concluída" },
            new STATUS { Id = 4, Nome = "Cancelada" }
        };
        }
    }
}
