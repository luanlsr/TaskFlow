using TaskFlow.Domain.Core.Entities;

namespace TaskFlow.Domain.Core.Interfaces
{
    public interface IService<T, TId> where T : EntityBase<TId>
    {
        Task<T> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);

        Task<IEnumerable<T>> FindAsync(Func<T, bool> predicate);
        Task<bool> ExistsAsync(Guid id);

        Task ValidateAsync(T entity);
    }
}
