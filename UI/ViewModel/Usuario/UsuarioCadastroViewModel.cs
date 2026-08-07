using Core.DTOs.Usuario;
using Core.Enums;
using Core.Interfaces;
using Core.Models;
using System.Windows;
using System.Windows.Input;
using UI.ViewModel.Base;

namespace UI.ViewModel.Usuario
{
    public class UsuarioCadastroViewModel : ViewModelBase
    {
        private readonly IUsuarioService _usuarioService;

        private string _senha = string.Empty;
        private bool _salvando;

        public UsuarioCompletoDTO Cadastro {  get; set; }

        public string Senha
        {
            get => _senha;
            set
            {
                if (_senha != value)
                {
                    _senha = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool Salvando
        {
            get => _salvando;
            private set
            {
                _salvando = value;
                OnPropertyChanged();
                CommandManager.InvalidateRequerySuggested();
            }
        }

        private AcaoFormulario _acaoFormulario;

        public AcaoFormulario AcaoFormulario
        {
            get => _acaoFormulario;
            private set
            {
                _acaoFormulario = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(ExibirCampoInativo));
            }
        }

        public bool ExibirCampoInativo =>
            AcaoFormulario != AcaoFormulario.Incluir;

        public ICommand SalvarCommand { get; }
        public ICommand CancelarCommand { get; }
        public event Action? FecharTela;

        public UsuarioCadastroViewModel(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
            Cadastro = new UsuarioCompletoDTO();
            Cadastro.Usuario.Inativo = false;
            AcaoFormulario = AcaoFormulario.Incluir;

            CriarPermissoes();

            SalvarCommand = new RelayCommand.RelayCommand(async _ => await SalvarAsync(), _ => !Salvando);
            CancelarCommand = new RelayCommand.RelayCommand(_ => FecharTela?.Invoke(), _ => !Salvando);
        }

        private void CriarPermissoes()
        {
            foreach (TipoTela tela in Enum.GetValues<TipoTela>())
            {
                Cadastro.Permissoes.Telas[tela.ToString()] =
                    new PermissaoTela
                    {
                        Visualizar = false,
                        Cadastrar = false,
                        Editar = false,
                        Excluir = false
                    };
            }
        }

        private async Task SalvarAsync()
        {
            try
            {
                Salvando = true;

                // O Service transformará esse valor em hash.
                Cadastro.Usuario.SenhaHash = Senha;

                await _usuarioService.InserirAsync(Cadastro);

                Senha = string.Empty;

                MessageBox.Show(
                    "Usuário cadastrado com sucesso.",
                    "Cadastro de usuário",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);

                FecharTela?.Invoke();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Dados inválidos",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Cadastro de usuário",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Não foi possível cadastrar o usuário.\n\n{ex.Message}",
                    "Erro",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
            finally
            {
                Salvando = false;
            }
        }
    }
}
