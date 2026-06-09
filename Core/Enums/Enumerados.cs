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
        Bug = 1,
        Sugestao = 2,
        Migracao = 3
    }
    public enum StatusTarefa
    {
        Pendente = 1,
        EmAndamento = 2,
        Concluida = 3,
        Cancelada = 4,
        Atrasada = 5
    }
    public enum TipoFormulario
    {
        Tarefas = 1,
        TarefasCad = 2
    }
    public enum TipoTela
    {
        Dashboard,
        Tarefas,
        Etapas,
        Usuarios
    }
}
