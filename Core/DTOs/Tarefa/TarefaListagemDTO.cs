using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.Tarefa
{
    public class TarefaListagemDTO
    {
        public Guid Id { get; set; }

        public string? Titulo { get; set; }

        public int TotalEtapas { get; set; }

        public int EtapasConcluidas { get; set; }

        public bool Atrasada { get; set; }

        public decimal PercentualConclusao { get; set; }
    }
}
