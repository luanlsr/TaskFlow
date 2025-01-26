using TaskFlow.Domain.Interfaces.Repository;

namespace TaskFlow.Domain.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IWorkItemRepository WorkItemRepository { get; }
        Task BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
        bool HasActiveTransaction();
        Task SaveChangesAsync();
    }
}
