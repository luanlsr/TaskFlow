using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Domain.Entities;
using TaskFlow.Domain.Interfaces.Repository;

namespace TaskFlow.Infrastructure.Data.Repositories
{
    public class WorkItemRepository : Repository<WorkItem, Guid>, IWorkItemRepository
    {
        public WorkItemRepository(ISession session) : base(session)
        {
        }

        public Task AddAsync(WorkItem entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistsAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<WorkItem>> FindAsync(Func<WorkItem, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<WorkItem>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<WorkItem> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(WorkItem entity)
        {
            throw new NotImplementedException();
        }
    }
}
