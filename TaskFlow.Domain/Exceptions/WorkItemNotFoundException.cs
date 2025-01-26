namespace TaskFlow.Domain.Core.Exceptions
{
    public class WorkItemNotFoundException : Exception
    {
        public WorkItemNotFoundException(Guid taskId)
            : base($"Task with ID {taskId} was not found.")
        {
        }
    }
}
