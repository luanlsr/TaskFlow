using TaskFlow.Domain.Core.Exceptions;

namespace TaskFlow.Domain.Exceptions
{
    public class WorkItemNotFoundException : DomainException
    {
        public WorkItemNotFoundException(Guid workItemId)
            : base($"O WorkItem com ID '{workItemId}' não foi encontrado.") { }
    }

    

   
}
