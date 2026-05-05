using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using UI.ViewModel.Tarefa;
using Core.Enums;
using UI.ViewModel.NavigationService;

namespace UI.View.Tarefa
{
    /// <summary>
    /// Lógica interna para TarefaPesquisaView.xaml
    /// </summary>
    public partial class TarefaPesquisaView : Window
    {
        public TarefaPesquisaView()
        {
            InitializeComponent();
            var navigation = new NavigationService();
            var vm = new TarefaPesquisaViewModel(navigation);
            DataContext = vm;
        }

        private void Vm_AbrirCadastro(
            TAREFA tarefa,
            AcaoFormulario acao)
        {
            var frm = new TarefaCadastroView(acao, tarefa);
            frm.ShowDialog();
        }
        private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is TarefaPesquisaViewModel vm
               && vm.TarefaSelecionada != null)
            {
                vm.AlterarCommand.Execute(null);
            }
        }

        private void cmdSair_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
