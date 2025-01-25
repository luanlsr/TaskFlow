namespace TaskFlow.Domain.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
        bool HasActiveTransaction();
        Task<int> SaveChangesAsync();
    }
}
