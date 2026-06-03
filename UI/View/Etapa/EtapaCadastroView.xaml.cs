using Core.Models;
using Microsoft.Extensions.DependencyInjection;
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
using UI.ViewModel.Etapa;

namespace UI.View
{
    /// <summary>
    /// Lógica interna para EtapaCadastroView.xaml
    /// </summary>
    public partial class EtapaCadastroView : Window
    {
        public EtapaCadastroView(Guid tarefaId, ETAPA? etapa = null)
        {
            InitializeComponent();

            var vm = ActivatorUtilities.CreateInstance<EtapaCadastroViewModel>(App.Services,tarefaId,etapa);

            vm.FecharTela += () => this.Close();

            DataContext = vm;
        }
    }
}
