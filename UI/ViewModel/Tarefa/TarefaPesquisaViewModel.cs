using Core.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Enums;
using Infrastruture.DAL;
using Application.Services;
using System.Windows.Input;
using System.Runtime.CompilerServices;
using UI.ViewModel.RelayCommand;
using System.Windows;

namespace UI.ViewModel.Tarefa
{
    public class TarefaPesquisaViewModel : INotifyPropertyChanged
    {
        private TAREFA _tarefaSelecionada;
        private string _filtroTitulo;
        private readonly TarefaService _service;
        private bool _procurarInativo;
        public string TextoCheckBox => ProcurarInativo ? "Procurar Inativo" : "Procurar Ativo";
        public ObservableCollection<TAREFA> ListaTarefas { get; set; }
        = new ObservableCollection<TAREFA>();
        public event Action<TAREFA, AcaoFormulario> AbrirCadastro;
        public TAREFA TarefaSelecionada
        {
            get => _tarefaSelecionada;
            set
            {
                _tarefaSelecionada = value;
                OnPropertyChanged();
            }
        }
        public TarefaPesquisaViewModel()
        {
            _service = new TarefaService();

            PesquisarCommand = new UI.ViewModel.RelayCommand.RelayCommand(Pesquisar);
            IncluirCommand = new UI.ViewModel.RelayCommand.RelayCommand(Incluir);
            AlterarCommand = new UI.ViewModel.RelayCommand.RelayCommand(Alterar, PodeAlterarExcluir);
            ExcluirCommand = new UI.ViewModel.RelayCommand.RelayCommand(Excluir, PodeAlterarExcluir);

            CarregarTarefas();
        }
        public string FiltroTitulo
        {
            get => _filtroTitulo;
            set
            {
                _filtroTitulo = value;
                OnPropertyChanged();
            }
        }
        public ICommand PesquisarCommand { get; }
        public ICommand IncluirCommand { get; }
        public ICommand AlterarCommand { get; }
        public ICommand ExcluirCommand { get; }
        public bool ProcurarInativo
        {
            get => _procurarInativo;
            set
            {
                _procurarInativo = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(TextoCheckBox));
                Pesquisar(null);
            }
        }
        private void CarregarTarefas()
        {

        }
        private async void Pesquisar(object obj)
        {
            try
            {
                ListaTarefas.Clear();

                var lista = await _service.ListarAsync(FiltroTitulo, ProcurarInativo);

                foreach (var item in lista)
                    ListaTarefas.Add(item);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void Incluir(object obj)
        {
            AbrirCadastro?.Invoke(null, AcaoFormulario.Incluir);
        }
        private void Alterar(object obj)
        {
            if (TarefaSelecionada == null) return;

            AbrirCadastro?.Invoke( TarefaSelecionada,AcaoFormulario.Alterar);
        }
        private void Excluir(object obj)
        {
            if (TarefaSelecionada == null) return;

            AbrirCadastro?.Invoke(
                TarefaSelecionada, AcaoFormulario.Excluir);
        }
        private bool PodeAlterarExcluir(object obj)
        {
            return TarefaSelecionada != null;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
