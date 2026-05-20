using Application.Services;
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
using UI.Helpers;

namespace UI.View
{
    /// <summary>
    /// Lógica interna para TarefaCadastroView.xaml
    /// </summary>
    public partial class TarefaCadastroView : Window
    {
        #region Variaveis

        private readonly TarefaService _service;
        private readonly AcaoFormulario _acao;
        private TAREFA _tarefa;
        //private readonly EtapaService _etapaService;
        //private ETAPA? _etapa;
        #endregion

        #region Construtor
        public TarefaCadastroView(AcaoFormulario acao,TAREFA? tarefaSelecionada)
        {
            InitializeComponent();

            _service = new TarefaService();
            _acao = acao;
            _tarefa = tarefaSelecionada ?? new TAREFA();
            DataContext = new TarefaCadastroViewModel(_tarefa);
            CarregarCombo();
        }

        #endregion

        #region funcoes
        private bool ValidaDados()
        {
            int? statusSelecionado = cmbStatus.SelectedValue != null
                ? Convert.ToInt32(cmbStatus.SelectedValue)
                : (int?)null;

            if (txtTitulo.Text == "")
            {
                txtTitulo.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtDescricao.Text))
            {
                txtDescricao.Focus();
                return false;
            }
            if (cmbDev.SelectedValue == null)
            {
                cmbDev.Focus();
                return false;
            }
            if (cmbStatus.SelectedValue == null)
            {
                cmbStatus.Focus();
                return false;
            }
            if (!DateInicio.SelectedDate.HasValue || !DatePrevisto.SelectedDate.HasValue)
            {
                return false;
            }
            if (statusSelecionado == (int)enStatusTarefa.Concluida && !DateFim.SelectedDate.HasValue)
            {
                MessageBox.Show("Status como concluído deve ter uma Data FIM",
                    "Atenção!",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);

                return false;
            }

            return true;
        }
        private TAREFA MontarModelo()
        {
            int? statusSelecionado = cmbStatus.SelectedValue != null
                ? Convert.ToInt32(cmbStatus.SelectedValue)
                : (int?)null;

            int? devSelecionado = cmbDev.SelectedValue != null
                ? Convert.ToInt32(cmbDev.SelectedValue)
                : (int?)null;

            bool Inativo = chkInativo.IsChecked ?? false;

            if (_acao == AcaoFormulario.Incluir)
            {
                return new TAREFA()
                {
                    Titulo = txtTitulo.Text,
                    Descricao = txtDescricao.Text,
                    StatusId = statusSelecionado,
                    DesenvolvedorId = devSelecionado,
                    DataInicio = Convert.ToDateTime(DateInicio.Text),
                    DataFimPrevisto = Convert.ToDateTime(DatePrevisto.Text),
                    DataFimReal = DateFim.SelectedDate,
                };
            }
            else
            {
                _tarefa.Titulo = txtTitulo.Text;
                _tarefa.StatusId = statusSelecionado;
                _tarefa.DesenvolvedorId = devSelecionado;
                _tarefa.Descricao = txtDescricao.Text;
                _tarefa.DataInicio = Convert.ToDateTime(DateInicio.Text);
                _tarefa.DataFimPrevisto = Convert.ToDateTime(DatePrevisto.Text);
                _tarefa.DataFimReal = DateFim.SelectedDate;
                _tarefa.Inativo = Inativo;
                return _tarefa;
            }
        }
        private async Task<bool> SalvarAsync()
        {
            if (!ValidaDados())
            {
                MessageBox.Show("Preencha todos os campos obrigatórios!",
                    "Erro",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return false;
            }

            var tarefa = MontarModelo();

            if (_acao == AcaoFormulario.Incluir)
            {
                var guidCriado = await _service.InserirAsync(tarefa);

                MessageBox.Show(
                    $"Tarefa criada com sucesso. Guid: {guidCriado}",
                    "Cadastro",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
            else if (_acao == AcaoFormulario.Alterar)
            {
                await _service.AtualizarAsync(tarefa);

                MessageBox.Show(
                    "Tarefa atualizada com sucesso.",
                    "Atualização",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }

            return true;
        }
        private void LimparTela()
        {
            txtTitulo.Text = "";
            txtDescricao.Text = "";
            cmbStatus.SelectedValue = enStatusTarefa.Pendente;
            cmbDev.SelectedValue = 0;
            chkInativo.IsChecked = false;
            //DateCadastro.Text = DateTime.Now.ToShortDateString();
            txtTitulo.Focus();

        }
        private void carregarTela()
        {
            txtTitulo.Focus();
        }
        private void CarregarDados()
        {
            codigoGuid.Text = _tarefa.Id.ToString();
            dataAtualizacao.Text = _tarefa.DataAtualizacao.ToString();
            txtTitulo.Text = _tarefa.Titulo;
            txtDescricao.Text = _tarefa.Descricao;
            cmbStatus.SelectedValue = _tarefa.StatusId;
            cmbDev.SelectedValue = _tarefa.DesenvolvedorId;
            dataCadastro.Text = _tarefa.DataCriacao.ToString();
            DateInicio.SelectedDate = _tarefa.DataInicio;
            DatePrevisto.SelectedDate = _tarefa.DataFimPrevisto;
            DateFim.SelectedDate = _tarefa.DataFimReal;
            chkInativo.IsChecked = _tarefa.Inativo;

        }
        private void CarregarCombo()
        {
            cmbStatus.ItemsSource = STATUSHELPER.ObterStatus();
            cmbDev.ItemsSource = LISTADESENVOLVEDOR.ObterDesenvolvedor();
        }
        private async Task AlterarStatusTarefa()
        {
            bool novoStatus = !_tarefa.Inativo;
            string acao = _tarefa.Inativo ? "ativar" : "inativar";

            var result = MessageBox.Show(
                $"Tem certeza que deseja {acao} a tarefa {_tarefa.Titulo}?",
                "Confirmação",
                MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
                await _service.AlterarStatusAsync(_tarefa.Id, novoStatus);

            this.Close();
        }
        #endregion
        #region Evento
        #region Evento Click
        private async void btnConfirmar_Click(object sender, RoutedEventArgs e)
        {
            if (await SalvarAsync())
                Close();
        }
        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #endregion
        #region Evento KeyDown
        private void txtTitulo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txtDescricao.Focus();
            }
        }
        private void txtDescricao_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                ComboBoxHelper.Open(cmbStatus, e);
        }
        private void cmbStatus_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                ComboBoxHelper.Open(cmbDev, e);
        }
        private void cmbDev_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                DateInicio.Focus();
        }
        private void DateInicio_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                DatePrevisto.Focus();
        }
        private void DatePrevisto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                DateFim.Focus();
        }
        private void DateFim_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                btnConfirmar.Focus();
        }
        #endregion
        #region Loaded
        
        private void tbEtapas_Loaded(object sender, RoutedEventArgs e)
        {

        }
        private void DatePrevisto_Loaded(object sender, RoutedEventArgs e)
        {
            DatePrevisto.AddHandler(UIElement.KeyDownEvent, new KeyEventHandler(DatePrevisto_KeyDown), true);
        }
        private void DateInicio_Loaded(object sender, RoutedEventArgs e)
        {
            DateInicio.AddHandler(UIElement.KeyDownEvent, new KeyEventHandler(DateInicio_KeyDown), true);
        }
        private void DateFim_Loaded(object sender, RoutedEventArgs e)
        {
            DateFim.AddHandler(UIElement.KeyDownEvent, new KeyEventHandler(DateFim_KeyDown), true);
        }
        #endregion
        #endregion


        private void chkInativoEtapa_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TarefaCadastro_Loaded(object sender, RoutedEventArgs e)
        {
            carregarTela();

            if (_acao != AcaoFormulario.Incluir)
                CarregarDados();

            if (_acao == AcaoFormulario.Incluir)
            {
                tbEtapas.Visibility = Visibility.Collapsed;
                chkInativo.Visibility = Visibility.Hidden;
            }


            //if (_acao == AcaoFormulario.Excluir)
                //AlterarStatusTarefa();
        }
    }
}
