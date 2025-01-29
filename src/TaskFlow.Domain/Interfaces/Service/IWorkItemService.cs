using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Domain.Core.Interfaces;
using TaskFlow.Domain.Entities;

namespace TaskFlow.Domain.Interfaces.Service
{
    public interface IWorkItemService : IService<WorkItem, Guid>
    {
        Task MarkAsCompletedAsync(Guid id);
    }
}
