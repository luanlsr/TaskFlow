using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Domain.ValueObjects;

namespace TaskFlow.Application.DTOs
{
    public class WorkItemDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public WorkItemStatus Status { get; set; }
        public DateTime DueDate { get; set; }
    }
}
