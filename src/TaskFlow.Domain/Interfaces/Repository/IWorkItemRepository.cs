using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Domain.Entities;
using TaskFlow.Domain.Core.Interfaces;

namespace TaskFlow.Domain.Interfaces.Repository
{
    public interface IWorkItemRepository : IRepository<WorkItem, Guid>
    {
    }
}
