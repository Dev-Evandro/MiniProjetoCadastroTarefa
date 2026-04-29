using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Enums
{
        public enum AcaoFormulario
        {
            Incluir = 1,
            Alterar = 2,
            Consultar = 3,
            Excluir = 4,
            Confirmar = 5,
            Cancelar = 6,
            Sair = 7
        }
        public enum TipoTarefa
        {
            Bug,
            Sugestao,
            Migracao
        }
        public enum enStatusTarefa
        {
            Pendente = 1,
            EmAndamento = 2,
            Concluida = 3,
            Cancelada = 4
        }
        public enum Formulario
        {
            Tarefas,
            TarefasCad,
        }
}
