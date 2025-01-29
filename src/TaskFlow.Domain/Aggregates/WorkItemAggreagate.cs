using TaskFlow.Domain.Core.Aggregates;
using TaskFlow.Domain.Entities;
using TaskFlow.Domain.ValueObjects;

namespace TaskFlow.Domain.Aggregates
{
    /// <summary>
    /// Raiz de agregado que encapsula a entidade WorkItem e possíveis objetos de valor relacionados.
    /// </summary>
    public class WorkItemAggregate : IAggregateRoot
    {
        public WorkItem WorkItem { get; private set; }

        // Exemplo: Você poderia ter coleções de comentários,
        // sub-tarefas, etc., todos fazendo parte do mesmo boundary.

        public WorkItemAggregate(WorkItem workItem)
        {
            WorkItem = workItem;
        }

        // Exemplo de método de domínio.
        public void CompleteWorkItem()
        {
            if (WorkItem.Status == WorkItemStatus.Completed)
                return;

            WorkItem.MarkAsCompletedAsync();
            WorkItem.UpdatedAt = DateTime.Now;
        }
    }
}
