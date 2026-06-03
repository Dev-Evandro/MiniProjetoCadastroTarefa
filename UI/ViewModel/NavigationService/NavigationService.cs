using Core.Enums;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static UI.ViewModel.Tarefa.TarefaPesquisaViewModel;
using UI.View;
using Microsoft.Extensions.DependencyInjection;
using UI.Helpers;

namespace UI.ViewModel.NavigationService
{
    public class NavigationService : INavigationService
    {
        public void AbrirTarefaCadastro(TAREFA? tarefa, AcaoFormulario acao)
        {
            var frm = ViewFactory.Resolve<TarefaCadastroView>();
            frm.Configurar(acao,tarefa);
            frm.ShowDialog();
        }
    }
}
