using System;

namespace TaskFlow.Domain.Events
{
    public class WorkItemCreatedEvent
    {
        public Guid WorkItemId { get; }
        public string Title { get; }
        public string Description { get; }
        public DateTime DueDate { get; }

        public WorkItemCreatedEvent(Guid workItemId, string title, string description, DateTime dueDate)
        {
            WorkItemId = workItemId;
            Title = title;
            Description = description;
            DueDate = dueDate;
        }
    }
}
