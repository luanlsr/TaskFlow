using MediatR;
using TaskFlow.Application.UseCases.Commands;
using TaskFlow.Domain.Core.Interfaces;
using TaskFlow.Domain.Entities;
using TaskFlow.Domain.Exceptions;
using TaskFlow.Domain.Interfaces.Service;

namespace TaskFlow.Application.Handlers
{
    public class DeleteWorkItemHandler : IRequestHandler<DeleteWorkItemCommand, Unit>
    {
        private readonly IWorkItemService _workItemService;

        public DeleteWorkItemHandler(IWorkItemService service)
        {
            _workItemService = service ?? throw new ArgumentNullException(nameof(service)); ;
        }

        public async Task<Unit> Handle(DeleteWorkItemCommand request, CancellationToken cancellationToken)
        {
            // Verifica se o WorkItem existe
            var workItem = await _workItemService.GetByIdAsync(request.Id);
            if (workItem == null)
            {
                throw new WorkItemNotFoundException(request.Id); // Exceção personalizada
            }

            // Exclui o WorkItem
            await _workItemService.DeleteAsync(workItem);

            // Retorna Unit.Value indicando que a operação foi concluída com sucesso
            return Unit.Value;
        }
    }
}
