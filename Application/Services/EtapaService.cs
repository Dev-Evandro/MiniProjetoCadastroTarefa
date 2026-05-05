using Core.Models;
using Infrastruture.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class EtapaService
    {
        private readonly EtapaDAL _dal;

        public EtapaService()
        {
            _dal = new EtapaDAL();
        }

        public async Task<List<ETAPA>> ListarAsync(
            Guid tarefaId,
            string? titulo,
            bool inativo = false)
        {
            return await _dal.ListarAsync(tarefaId, titulo, inativo);
        }

        public async Task<ETAPA?> ConsultarAsync(Guid id, bool inativo = false)
        {
            return await _dal.ConsultarAsync(id, inativo);
        }

        public async Task InserirAsync(ETAPA etapa)
        {
            ValidarEtapa(etapa);

            etapa.Id = Guid.NewGuid();

            await _dal.InserirAsync(etapa);
        }

        public async Task AtualizarAsync(ETAPA etapa)
        {
            ValidarEtapa(etapa);

            await _dal.AtualizarAsync(etapa);
        }

        public async Task AlterarStatusAsync(Guid id, bool inativo)
        {
            await _dal.AlterarStatusAsync(id, inativo);
        }

        private void ValidarEtapa(ETAPA etapa)
        {


            if (etapa.TarefaId == Guid.Empty)
                throw new Exception("Tarefa não informada.");

            if (string.IsNullOrWhiteSpace(etapa.Titulo))
                throw new Exception("Título é obrigatório.");

            if (etapa.DataFimPrevisto.HasValue &&
                etapa.DataFimPrevisto < etapa.DataInicio)
                throw new Exception("Data fim previsto não pode ser menor que data início.");
        }
    }
}
