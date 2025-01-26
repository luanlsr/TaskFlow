namespace TaskFlow.Domain.Events
{
    public class WorkItemCompletedEvent
    {
        public Guid WorkItemId { get; }
        public DateTime CompletedAt { get; }

        public WorkItemCompletedEvent(Guid taskId)
        {
            WorkItemId = taskId;
            CompletedAt = DateTime.UtcNow;
        }

        public WorkItemCompletedEvent(int id)
        {
        }
    }
}
