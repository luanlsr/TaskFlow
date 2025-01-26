using TaskFlow.Domain.Core.Entities;
using TaskFlow.Domain.ValueObjects;

namespace TaskFlow.Domain.Entities
{
    public class WorkItem : EntityBase<Guid>
    {
        public string Title { get; private set; }
        public string Description { get; private set; }
        public WorkItemStatus Status { get; private set; }
        public DateTime DueDate { get; private set; }

        public WorkItem() : base() { }

        public WorkItem(string title, string description, DateTime dueDate)
            : base(Guid.NewGuid())
        {
            Title = title;
            Description = description;
            DueDate = dueDate;
            Status = WorkItemStatus.Pending;
        }

        // Exemplo de método de domínio para concluir uma tarefa
        public void MarkAsCompletedAsync()
        {
            if (Status == WorkItemStatus.Completed)
                return; // Evita "reconclusão"

            Status = WorkItemStatus.Completed;
            UpdatedAt = DateTime.UtcNow;
        }

        // Exemplo de método para atualizar propriedades
        public void UpdateWorkItem(string title, string description, DateTime dueDate)
        {
            Title = title;
            Description = description;
            DueDate = dueDate;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
