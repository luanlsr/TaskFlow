using NHibernate;
using TaskFlow.Domain.Core.Interfaces;
using System;
using System.Threading.Tasks;
using Polly;
using TaskFlow.Infrastructure.Data.Repositories;
using TaskFlow.Domain.Interfaces.Repository;

namespace TaskFlow.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ISession _session;
        private ITransaction _transaction;
        public IWorkItemRepository WorkItemRepository { get; }

        public UnitOfWork(ISession session)
        {
            _session = session;
            _transaction = null;

            WorkItemRepository = new WorkItemRepository(session);
        }

        public async Task BeginTransactionAsync()
        {
            if (_transaction == null)
            {
                _transaction = _session.BeginTransaction();
            }
        }

        // Confirma a transação
        public async Task CommitAsync()
        {
            if (_transaction == null || !_transaction.IsActive)
                throw new InvalidOperationException("No active transaction found.");

            try
            {
                await _transaction.CommitAsync();
            }
            catch (Exception)
            {
                await RollbackAsync();
                throw;
            }
        }

        // Reverte a transação
        public async Task RollbackAsync()
        {
            if (_transaction != null && _transaction.IsActive)
            {
                await _transaction.RollbackAsync();
            }
        }

        // Verifica se existe uma transação ativa
        public bool HasActiveTransaction()
        {
            return _transaction != null && _transaction.IsActive;
        }

        // Salva as alterações (para NHibernate, este é um commit de mudanças)
        public async Task SaveChangesAsync()
        {
            if (_transaction == null || !_transaction.IsActive)
                throw new InvalidOperationException("No active transaction found.");

            // O SaveChanges no NHibernate seria equivalente ao flush
            await _session.FlushAsync();
        }

        // Dispose para garantir que qualquer recurso da sessão ou transação seja liberado
        public void Dispose()
        {
            _transaction?.Dispose();
            _session?.Dispose();
        }
    }
}
