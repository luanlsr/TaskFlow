using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskFlow.Domain.Entities;
using TaskFlow.Domain.Interfaces.Repository;
using TaskFlow.Infrastructure.Data.Context;

namespace TaskFlow.Infrastructure.Data.Repositories
{
    public class WorkItemRepository : Repository<WorkItem, Guid>, IWorkItemRepository
    {
        public WorkItemRepository(AppDbContext context) : base(context)
        {
        }
    }
}
