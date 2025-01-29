using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskFlow.CrossCutting.Logging;
using TaskFlow.Domain.Interfaces.Service;
using TaskFlow.Application.UseCases.Commands;
using TaskFlow.Application.UseCases.Queries;
using TaskFlow.CrossCutting.Logging.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace TaskFlow.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class WorkItemController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IWorkItemService _workItemService;

        private readonly ISerilogLoggerService _serilogLogger;
        private readonly IConsoleLoggerService _consoleLogger;

        public WorkItemController(
            IMediator mediator,
            IWorkItemService workItemService,
            ISerilogLoggerService serilogLogger,
            IConsoleLoggerService consoleLogger
        )
        {
            _mediator = mediator;
            _workItemService = workItemService;
            _serilogLogger = serilogLogger;
            _consoleLogger = consoleLogger;
        }

        /// <summary>
        /// Cria um WorkItem via MediatR (Command)
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateWorkItem([FromBody] CreateWorkItemCommand command)
        {
            _serilogLogger.Information("Receiving request to create a WorkItem via MediatR command.");
            var result = await _mediator.Send(command);

            _consoleLogger.Information($"WorkItem created with ID: {result.Id}");
            return CreatedAtAction(nameof(GetWorkItemById), new { id = result.Id }, result);
        }

        /// <summary>
        /// Lista todos os WorkItems via MediatR (Query)
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAllWorkItems()
        {
            _serilogLogger.Information("Getting all work items via MediatR query.");
            var result = await _mediator.Send(new GetAllWorkItemsQuery());
            return Ok(result);
        }

        /// <summary>
        /// Busca um WorkItem por ID via MediatR (Query)
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetWorkItemById(Guid id)
        {
            _serilogLogger.Debug($"Getting work item {id} via MediatR query.");
            var result = await _mediator.Send(new GetWorkItemByIdQuery(id));

            if (result == null)
            {
                _consoleLogger.Warning($"WorkItem {id} not found.");
                return NotFound();
            }

            return Ok(result);
        }

        /// <summary>
        /// Atualiza um WorkItem via MediatR (Command)
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWorkItem(Guid id, [FromBody] UpdateWorkItemCommand command)
        {
            command.Id = id;
            _serilogLogger.Information($"Updating work item {id} via MediatR command.");
            var result = await _mediator.Send(command);

            return Ok(result);
        }

        /// <summary>
        /// Deleta um WorkItem via MediatR (Command)
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkItem(Guid id)
        {
            _consoleLogger.Information($"Deleting work item {id} via MediatR command.");
            await _mediator.Send(new DeleteWorkItemCommand(id));
            return NoContent();
        }

        /// <summary>
        /// Exemplo de endpoint que chama DIRETAMENTE o serviço de domínio
        /// sem passar por um Command/Query do MediatR.
        /// </summary>
        [HttpPost("complete/{id}")]
        public async Task<IActionResult> MarkAsCompleted(Guid id)
        {
            _serilogLogger.Information($"Marking work item {id} as completed via domain service.");
            try
            {
                await _workItemService.MarkAsCompletedAsync(id);
                _consoleLogger.Information($"WorkItem {id} marked as completed.");

                return Ok(new { Message = $"WorkItem {id} completed successfully." });
            }
            catch (Exception ex)
            {
                _serilogLogger.Error("Error completing work item", ex);
                return BadRequest(new { Error = ex.Message });
            }
        }
    }
}
