using Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UI.Helpers;
using UI.View.Tarefa;
using UI.ViewModel.Tarefa;

namespace UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var configService = new ConfiguracaoService();
                configService.Carregar();

                MessageBox.Show("Configuração carregada com sucesso!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
                System.Windows.Application.Current.Shutdown();
            }
        }
        private void AbrirTarefas_Click(object sender, RoutedEventArgs e)
        {
            var f = App.Services?.GetRequiredService<TarefaPesquisaView>();

            f?.Show();
            this.Close();
        }
    }
}