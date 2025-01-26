using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Domain.Entities;

namespace TaskFlow.Domain.Interfaces.Repository
{
    public interface IWorkItemRepository
    {
        Task<WorkItem> GetByIdAsync(Guid id);
        Task<IEnumerable<WorkItem>> GetAllAsync();
        Task AddAsync(WorkItem entity);
        Task UpdateAsync(WorkItem entity);
        Task DeleteAsync(Guid id);

        Task<IEnumerable<WorkItem>> FindAsync(Func<WorkItem, bool> predicate);
        Task<bool> ExistsAsync(Guid id);
    }
}
