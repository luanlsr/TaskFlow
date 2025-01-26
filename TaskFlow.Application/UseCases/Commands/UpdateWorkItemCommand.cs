using MediatR;
using TaskFlow.Application.DTOs;

namespace TaskFlow.Application.UseCases.Commands
{
    public class UpdateWorkItemCommand : IRequest<WorkItemDTO>
    {
        public Guid Id { get; set; }  // Adicione esta propriedade
        public string Title { get; set; }
        public string Description { get; set; }
        // Inclua quaisquer outros campos relevantes para a atualização
    }
}
