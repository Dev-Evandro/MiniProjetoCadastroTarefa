using Core.Models;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using UI.View.Usuario;
using UI.ViewModel.Base;
using UI.ViewModel.RelayCommand;

namespace UI.ViewModel.Usuario
{
    public class UsuarioPesquisaViewModel : ViewModelBase
    {
        private string? _filtro;
        private bool _procurarInativo;
        private USUARIO? _usuarioSelecionado;

        public string? Filtro
        {
            get => _filtro;
            set
            {
                _filtro = value;
                OnPropertyChanged();
            }
        }

        public bool ProcurarInativo
        {
            get => _procurarInativo;
            set
            {
                _procurarInativo = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<USUARIO> ListaUsuarios { get; } = [];

        public USUARIO? UsuarioSelecionado
        {
            get => _usuarioSelecionado;
            set
            {
                _usuarioSelecionado = value;
                OnPropertyChanged();
            }
        }

        public ICommand PesquisarCommand { get; }

        public ICommand IncluirCommand { get; }

        public ICommand AlterarCommand { get; }

        public ICommand InativarCommand { get; }

        public ICommand SairCommand { get; }

        public event Action? FecharTela;

        public UsuarioPesquisaViewModel()
        {
            PesquisarCommand = new RelayCommand.RelayCommand(
                _ => { });

            IncluirCommand = new RelayCommand.RelayCommand(Incluir);

            AlterarCommand = new RelayCommand.RelayCommand(
                _ => { },
                _ => false);

            InativarCommand = new RelayCommand.RelayCommand(
                _ => { },
                _ => false);

            SairCommand = new RelayCommand.RelayCommand(
                _ => FecharTela?.Invoke());
        }

        private void Incluir(object? parametro)
        {
            var view =
                App.Services.GetRequiredService<UsuarioCadastroView>();

            if (parametro is Window janelaPesquisa)
            {
                view.Owner = janelaPesquisa;
            }

            view.ShowDialog();
        }
    }
}