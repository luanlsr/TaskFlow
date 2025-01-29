using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Domain.ValueObjects;

namespace TaskFlow.Domain.Events
{
    public class WorkItemUpdatedEvent
    {
        public Guid WorkItemId { get; }
        public string Title { get; }
        public string Description { get; }
        public DateTime DueDate { get; }
        public WorkItemStatus Status { get; }

        public WorkItemUpdatedEvent(Guid workItemId, string title, string description, DateTime dueDate, WorkItemStatus status)
        {
            WorkItemId = workItemId;
            Title = title;
            Description = description;
            DueDate = dueDate;
            Status = status;
        }
    }
}
