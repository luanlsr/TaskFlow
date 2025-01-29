using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using TaskFlow.Domain.Core.Interfaces;
using TaskFlow.Domain.Interfaces.Repository;
using TaskFlow.Infrastructure.Data.EntityFramework.Context;

namespace TaskFlow.Infrastructure.Data.EntityFramework
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly AppDbContext _context;
        private readonly IWorkItemRepository _workItemRepository;
        private IDbContextTransaction _currentTransaction;

        public UnitOfWork(AppDbContext context, IWorkItemRepository workItemRepository)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _workItemRepository = workItemRepository ?? throw new ArgumentNullException(nameof(workItemRepository));
        }

        public IWorkItemRepository WorkItemRepository => _workItemRepository;

        public async Task BeginTransactionAsync()
        {
            if (_currentTransaction == null)
            {
                _currentTransaction = await _context.Database.BeginTransactionAsync();
            }
        }

        public async Task<int> CommitAsync()
        {
            try
            {
                var result = await _context.SaveChangesAsync();
                if (_currentTransaction != null)
                {
                    await _currentTransaction.CommitAsync();
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
                return result;
            }
            catch
            {
                await RollbackAsync();
                throw;
            }
        }

        public async Task RollbackAsync()
        {
            if (_currentTransaction != null)
            {
                await _currentTransaction.RollbackAsync();
                _currentTransaction.Dispose();
                _currentTransaction = null;
            }
        }

        public bool HasActiveTransaction()
        {
            return _currentTransaction != null;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _currentTransaction?.Dispose();
            _context?.Dispose();
        }
    }
}
