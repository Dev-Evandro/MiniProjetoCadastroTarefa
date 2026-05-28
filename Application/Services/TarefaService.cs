using Core.DTOs.Dashboard;
using Core.Models;
using Infrastruture.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class TarefaService
    {
        private readonly TarefaDAL _dal;

        public TarefaService(TarefaDAL dal)
        {
            _dal = dal;
        }
        public async Task<Guid> InserirAsync(TAREFA tarefa)
        {
            tarefa.Id = Guid.NewGuid();
            tarefa.DataCriacao = DateTime.Now;
            tarefa.DataAtualizacao = DateTime.Now;
            tarefa.Inativo = false;

            await _dal.InserirAsync(tarefa);

            return tarefa.Id;
        }

        public async Task<IEnumerable<TAREFA>> ListarAsync(string? filtroTitulo, bool procurarInativo)
        {
            return await _dal.ListarAsync(filtroTitulo, procurarInativo);
        }
        public async Task AtualizarAsync(TAREFA tarefa)
        {
            tarefa.DataAtualizacao = DateTime.Now;
            await _dal.AtualizarAsync(tarefa);
        }
        public async Task<TAREFA?> ConsultarTarefa(Guid guid, bool inativo = false)
        {
            return await _dal.ConsultarTarefaAsync(guid, inativo);
        }
        public async Task<TAREFA?> ConsultarCompletoAsync(Guid id, bool inativo = false)
        {
            return await _dal.ConsultarCompletoAsync(id, inativo);
        }

        public async Task AlterarStatusAsync(Guid id, bool inativo)
        {
            await _dal.AlterarStatusAsync(id, inativo);
        }
        public async Task<DashboardDTO> ObterDashboardAsync()
        {
            return await _dal.ObterDashboardAsync();
        }

    }
}
