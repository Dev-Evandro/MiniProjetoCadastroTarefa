using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class ETAPA
    {
        public Guid Id { get; set; }
        public Guid TarefaId { get; set; }
        public string Titulo { get; set; }
        public string? Descricao { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime? DataFimPrevisto { get; set; }
        public DateTime? DataFimReal { get; set; }
        public int Ordem { get; set; }
        public bool Inativo { get; set; }
        public virtual TAREFA? Tarefa { get; set; }
        public string Status
        {
            get
            {
                if (DataFimReal != null)
                    return "Concluído";

                if (DataFimPrevisto < DateTime.Now)
                    return "Atrasado";

                return "Pendente";
            }
        }
        public string TempoRestante
        {
            get
            {
                if (!DataFimPrevisto.HasValue)
                    return "Sem prazo";

                var dias = (DataFimPrevisto.Value.Date - DateTime.Today).Days;

                if (dias > 0)
                    return $"{dias} dias restantes";

                if (dias < 0)
                    return $"{Math.Abs(dias)} dias em atraso";

                return "Vence hoje";
            }
        }
    }
}
