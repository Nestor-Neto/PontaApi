using Application.Models;
using Application.Util;
using Domain.Entities;
using Domain.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Servico.Validadores;

namespace Application.Controllers
{
    /// <summary>
    /// Controller de implementações das tarefas.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class TarefasController : ControllerBase
    {
        private readonly IServiceTarefas   _serviceBase;

        private string logLabel = "[ROTA: TarefasController/{0}] [USUARIO: {1} ] [DADOS: {2} ] ";
        private List<string> listLogs = new();
        private string logLabelController;

        private readonly ILogger<Tarefas> _logger;
        /// <summary>
        /// Inicializa a instância do controlador.
        /// </summary>
        /// <param name="taskService">O serviço das tarefas a ser injetado.</param>
        public TarefasController(IServiceTarefas servicoBase, ILogger<Tarefas> logger)
        {
            _serviceBase = servicoBase;
            _logger = logger;
        }
        /// <summary>
        /// Obtém todas as tarefas.
        /// </summary>
        [HttpGet]
        [Route("ListAll")]
        [Authorize]
        public IActionResult ListAll()
        {
            logLabelController = "ListAll";
            return Execute(() => _serviceBase.ListAll<SaidaTarefaModel>());
        }
        /// <summary>
        /// Busca tarefas com base no seu ID de status.
        /// </summary>
        /// <param name="id">O ID do status da tarefa.</param>
        [HttpGet]
        [Route("SelectStatus")]
        [Authorize]
        public IActionResult SelectStatus([FromQuery] int? Idstatus)
        {
            logLabelController = "SelectStatus";
            return Execute(() => _serviceBase.SelectStatus<SaidaTarefaModel>(Idstatus));
        }

        /// <summary>
        /// criação da Tarefa
        /// </summary>
        /// <param name="model">parametros para a criação da Tarefa.</param>
        /// <returns>Tarefa criada.</returns>
        [HttpPost]
        [Route("Insert")]
        [Authorize]
        public IActionResult Insert([FromBody] EntradaTarefaModel model)
        {
            logLabelController = "Insert";
            return Execute(() => _serviceBase.Insert<EntradaTarefaModel, Tarefas, ValidatorTarefas>(model));
       
        }
        /// <summary>
        /// Alteração da Tarefa
        /// </summary>
        /// <param name="model">parametros para a alteração da Tarefa.</param>
        /// <returns>Tarefa alterada.</returns>
        [HttpPost]
        [Route("Update")]
        [Authorize]
        public IActionResult Update([FromBody] AtualizacaoTarefaModel model)
        {
            if (model == null)
                return NotFound();

            logLabelController = "Update";

            return Execute(() => _serviceBase.Update<AtualizacaoTarefaModel, Tarefas, ValidatorTarefas>(model));
        }
        /// <summary>
        /// Exclui a Tarefa
        /// </summary>
        /// <param name="model">parametro ID da Tarefa.</param>
        /// <returns>Tarefa deletada.</returns>
        [HttpDelete]
        [Route("Delete")]
        [Authorize]
        public IActionResult Delete(int id)
        {
            try
            {
                if (_serviceBase.Delete(id))
                {
                    listLogs.AddRange(Log.Logs("Delete | "+ HttpContext.User.Identity.Name +" | " + id.ToString() + "| Sucesso"));
                   _logger.LogInformation(Log.LabelFormat(logLabel, listLogs));

                    return Ok("Tarefa deletada com sucesso!");
                }

               listLogs.AddRange(Log.Logs("Delete | " + HttpContext.User.Identity.Name + " | " + id.ToString() + "| Erro"));
               _logger.LogInformation(Log.LabelFormat(logLabel, listLogs));

                return BadRequest("Erro, entre em contato administrador do sistema!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + "\r\n\r\n" + ex.InnerException?.Message + "\r\n\r\nErro, entre em contato administrador do sistema!");
            }

        }

        
        private IActionResult Execute(Func<object> func)
        {
            try
            {
                var result = func();
               listLogs.AddRange(Log.Logs(logLabelController + "|"+ HttpContext.User.Identity.Name + "|"+ JsonConvert.SerializeObject(result) + " | Sucesso"));
               _logger.LogInformation(Log.LabelFormat(logLabel, listLogs));

                return Ok(result);
            }
            catch (Exception ex)
            {
                listLogs.AddRange(Log.Logs(logLabelController+ "|" + HttpContext.User.Identity.Name + "|" + ex.Message + "\r\n\r\n" + ex.InnerException?.Message + "| Erro"));
               _logger.LogCritical(Log.LabelFormat(logLabel, listLogs));
                return BadRequest(ex.Message + "\r\n\r\n" + ex.InnerException?.Message + "\r\n\r\nErro, entre em contato Administrador do sistema!");
            }
        }
    }
}
