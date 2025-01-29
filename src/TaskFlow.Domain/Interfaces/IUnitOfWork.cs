using TaskFlow.Domain.Interfaces.Repository;

namespace TaskFlow.Domain.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IWorkItemRepository WorkItemRepository { get; }
        Task BeginTransactionAsync();
        Task<int> CommitAsync();
        Task RollbackAsync();
        bool HasActiveTransaction();
        Task SaveChangesAsync();
    }
}
