using TaskFlow.Domain.Core.Interfaces;
using TaskFlow.Domain.Entities;
using TaskFlow.Domain.Exceptions; // Caso precise lançar alguma exceção
using TaskFlow.Domain.Interfaces;
using System;
using TaskFlow.Domain.Interfaces.Repository;
using TaskFlow.Domain.Interfaces.Service;
using TaskFlow.Domain.Core.Exceptions;

namespace TaskFlow.Domain.Services
{
    public class WorkItemService : IWorkItemService
    {
        private readonly IUnitOfWork _unitOfWork;

        public WorkItemService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task AddAsync(WorkItem entity)
        {
            return _unitOfWork.WorkItemRepository.AddAsync(entity);
        }

        public Task DeleteAsync(WorkItem workItem)
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

        public Task MarkAsCompletedAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(WorkItem entity)
        {
            throw new NotImplementedException();
        }

        public Task ValidateAsync(WorkItem entity)
        {
            throw new NotImplementedException();
        }
    }
}
