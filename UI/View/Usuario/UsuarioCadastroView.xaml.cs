using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using UI.ViewModel.Usuario;


namespace UI.View.Usuario
{
    /// <summary>
    /// Lógica interna para UsuarioCadastroView.xaml
    /// </summary>
    public partial class UsuarioCadastroView : Window
    {
        private readonly UsuarioCadastroViewModel _viewModel;

        public UsuarioCadastroView(
            UsuarioCadastroViewModel viewModel)
        {
            InitializeComponent();

            _viewModel = viewModel;
            DataContext = _viewModel;

            _viewModel.FecharTela += FecharTela;
        }
        private void FecharTela()
        {
            Close();
        }
        protected override void OnClosed(EventArgs e)
        {
            _viewModel.FecharTela -= FecharTela;
            base.OnClosed(e);
        }

        private void txtSenha_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (sender is PasswordBox passwordBox)
            {
                _viewModel.Senha = passwordBox.Password;
            }
        }
    }
}
