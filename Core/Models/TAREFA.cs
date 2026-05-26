using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class TAREFA
    {
        public Guid Id { get; set; }

        public string? Titulo { get; set; }

        public string? Descricao { get; set; }

        public DateTime DataInicio { get; set; }

        public DateTime? DataFimPrevisto { get; set; }

        public DateTime? DataFimReal { get; set; }

        public int? StatusId { get; set; }
        public bool Inativo { get; set; }
        public int? DesenvolvedorId { get; set; }

        public DateTime DataCriacao { get; set; }

        public DateTime? DataAtualizacao { get; set; }

        // 🔗 Relacionamentos
        public string Status
        {
            get
            {
                var status = STATUSHELPER.ObterStatus().FirstOrDefault(x => x.Id == StatusId);

                return status?.Nome ?? "";
            }
        }
        public string NomeDesenvolvedor
        {
            get 
            {

                var desenvolvedor = LISTADESENVOLVEDOR
                    .ObterDesenvolvedor()
                    .FirstOrDefault(x => x.Id == DesenvolvedorId);

                return desenvolvedor?.Nome ?? "";
            }
        }
        public virtual ICollection<ETAPA> Etapas { get; set; }
            = new List<ETAPA>();
    }
}
