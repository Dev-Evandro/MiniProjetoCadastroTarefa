using Application.Services;
using Core.Models;
using Infrastruture.DAL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UI.View;
using UI.ViewModel.Base;
using UI.ViewModel.RelayCommand;

namespace UI.ViewModel.Tarefa
{
    public class TarefaCadastroViewModel : ViewModelBase
    {

        #region EtapaView
        private readonly EtapaService _etapaService;
        public TAREFA Tarefa { get; set; }
        public ObservableCollection<ETAPA> ListaEtapas { get; set; }
        private ETAPA? _etapaSelecionada;
        public ETAPA? EtapaSelecionada
        {
            get => _etapaSelecionada;
            set
            {
                _etapaSelecionada = value;
                OnPropertyChanged();
            }
        }
        private int _abaSelecionada;
        public int AbaSelecionada
        {
            get => _abaSelecionada;
            set
            {
                _abaSelecionada = value;
                OnPropertyChanged();
                if (_abaSelecionada == 1)
                {
                    _ = CarregarEtapas();
                }
            }
        }
        private string? _filtroDescricaoEtapa;
        public string? FiltroDescricaoEtapa
        {
            get => _filtroDescricaoEtapa;
            set
            {
                _filtroDescricaoEtapa = value;
                OnPropertyChanged();

                _ = CarregarEtapas();
            }
        }
        private bool _mostrarInativoEtapa;
        public bool MostrarInativoEtapa
        {
            get => _mostrarInativoEtapa;
            set
            {
                _mostrarInativoEtapa = value;
                OnPropertyChanged();
            }
        }
        public ICommand ConsultarEtapaCommand { get; }
        public ICommand AdicionarEtapaCommand { get; }
        public ICommand AlterarEtapaCommand { get; }
        public ICommand RemoverEtapaCommand { get; }
        public TarefaCadastroViewModel(TAREFA tarefa, EtapaService etapaService)
        {
            _etapaService = etapaService;

            Tarefa = tarefa;

            ListaEtapas = new ObservableCollection<ETAPA>();

            ConsultarEtapaCommand = new RelayCommand.RelayCommand(async _ => await CarregarEtapas());
            AdicionarEtapaCommand = new RelayCommand.RelayCommand(AdicionarEtapa);
            AlterarEtapaCommand = new RelayCommand.RelayCommand(AlterarEtapa, _ => EtapaSelecionada != null);
            RemoverEtapaCommand = new RelayCommand.RelayCommand(RemoverEtapa, _ => EtapaSelecionada != null);
        }
        private async Task CarregarEtapas()
        {
            if (Tarefa == null)
                return;

            ListaEtapas.Clear();

            var lista = await _etapaService.ListarAsync(
                Tarefa.Id,
                FiltroDescricaoEtapa,
                MostrarInativoEtapa);

            foreach (var item in lista)
                ListaEtapas.Add(item);
        }
        private async void AdicionarEtapa(object? obj)
        {
            var frm = new EtapaCadastroView(Tarefa.Id);
            frm.ShowDialog();

            await CarregarEtapas();
        }
        private void AlterarEtapa(object? obj)
        {
            if (EtapaSelecionada == null)
                return;

            var frm = new EtapaCadastroView(Tarefa.Id, EtapaSelecionada);
            frm.ShowDialog();

            _ = CarregarEtapas();
        }
        private async void RemoverEtapa(object? obj)
        {
            if (EtapaSelecionada == null)
                return;

            await _etapaService.AlterarStatusAsync(
                EtapaSelecionada.Id,
                true);

            await CarregarEtapas();
        }
        #endregion
    }
}
