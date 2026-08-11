using System.Windows;
using UI.ViewModel.Usuario;

namespace UI.View.Usuario
{
    public partial class UsuarioPesquisaView : Window
    {
        private readonly UsuarioPesquisaViewModel _viewModel;

        public UsuarioPesquisaView(
            UsuarioPesquisaViewModel viewModel)
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
    }
}