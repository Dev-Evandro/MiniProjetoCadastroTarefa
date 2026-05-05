using Application.Services;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UI.ViewModel.Base;

namespace UI.ViewModel.Etapa
{
    public class EtapaCadastroViewModel : ViewModelBase
    {

        private readonly EtapaService _service;

        public ETAPA Etapa { get; set; }

        public ICommand SalvarCommand { get; }
        public ICommand CancelarCommand { get; }

        public event Action? FecharTela;

        public EtapaCadastroViewModel(Guid tarefaId, ETAPA? etapa = null)
        {
            _service = new EtapaService();

            Etapa = etapa ?? new ETAPA
            {
                Id = Guid.Empty,
                TarefaId = tarefaId,
                DataInicio = DateTime.Today,
                Inativo = false
            };

            SalvarCommand = new RelayCommand.RelayCommand(async _ => await Salvar());
            CancelarCommand = new RelayCommand.RelayCommand(_ => FecharTela?.Invoke());
        }

        private async Task Salvar()
        {
            if (Etapa.Id == Guid.Empty)
                await _service.InserirAsync(Etapa);
            else
                await _service.AtualizarAsync(Etapa);

            FecharTela?.Invoke();
        }
    }
}
