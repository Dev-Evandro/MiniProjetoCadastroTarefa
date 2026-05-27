using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.Dashboard
{
    public class DashboardDTO
    {
        public int TotalTarefas { get; set; }

        public int Concluidas { get; set; }

        public int Pendentes { get; set; }

        public int Atrasadas { get; set; }
    }
}
