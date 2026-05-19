using Microsoft.AspNetCore.Mvc;
using Application.Services;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TarefaController : ControllerBase
    {
        private readonly TarefaService _service;

        public TarefaController(TarefaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Listar(
            [FromQuery] string filtroTitulo = "",
            [FromQuery] bool procurarInativo = false)
        {
            var tarefas = await _service.ListarAsync(
                filtroTitulo,
                procurarInativo);

            return Ok(tarefas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Consultar(Guid id)
        {
            var tarefa = await _service.ConsultarTarefa(id);

            if (tarefa == null)
                return NotFound();

            return Ok(tarefa);
        }
        [HttpGet("{id}/completo")]
        public async Task<IActionResult> ConsultarCompleto(Guid id)
        {
            var tarefa = await _service.ConsultarCompletoAsync(id);

            if (tarefa == null)
                return NotFound();

            return Ok(tarefa);
        }


    }
}
