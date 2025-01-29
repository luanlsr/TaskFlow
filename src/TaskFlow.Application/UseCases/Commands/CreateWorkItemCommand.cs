using MediatR;
using TaskFlow.Application.DTOs;

namespace TaskFlow.Application.UseCases.Commands
{
    public class CreateWorkItemCommand : IRequest<WorkItemDTO>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }

        // Construtor opcional para inicialização
        public CreateWorkItemCommand(string title, string description, DateTime dueDate)
        {
            Title = title;
            Description = description;
            DueDate = dueDate;
        }

        // Construtor vazio para frameworks (opcional)
        public CreateWorkItemCommand() { }
    }
}
