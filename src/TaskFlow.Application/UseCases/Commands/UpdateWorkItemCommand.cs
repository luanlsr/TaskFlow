using MediatR;
using TaskFlow.Application.DTOs;

namespace TaskFlow.Application.UseCases.Commands
{
    public class UpdateWorkItemCommand : IRequest<WorkItemDTO>
    {
        public Guid Id { get; set; } // Identificador do WorkItem
        public string Title { get; set; } // Título atualizado
        public string Description { get; set; } // Descrição atualizada
        public DateTime DueDate { get; set; } // Nova data de vencimento
        public string Status { get; set; } // Status atualizado (se necessário)

        public UpdateWorkItemCommand() { }

        public UpdateWorkItemCommand(Guid id, string title, string description, DateTime dueDate, string status)
        {
            Id = id;
            Title = title;
            Description = description;
            DueDate = dueDate;
            Status = status;
        }
    }
}
