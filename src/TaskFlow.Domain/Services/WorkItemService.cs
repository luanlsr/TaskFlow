using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskFlow.Domain.Core.Interfaces;
using TaskFlow.Domain.Entities;
using TaskFlow.Domain.Exceptions;
using TaskFlow.Domain.Interfaces.Service;
using TaskFlow.Domain.Events;
using TaskFlow.Domain.ValueObjects;

namespace TaskFlow.Domain.Services
{
    public class WorkItemService : IWorkItemService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMessagePublisher _messagePublisher;

        public WorkItemService(IUnitOfWork unitOfWork, IMessagePublisher messagePublisher)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _messagePublisher = messagePublisher ?? throw new ArgumentNullException(nameof(messagePublisher));
        }

        public async Task AddAsync(WorkItem entity)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                await _unitOfWork.WorkItemRepository.AddAsync(entity);

                if (await EnsureTransactionCommitted())
                {
                    var workItemCreatedEvent = new WorkItemCreatedEvent(entity.Id, entity.Title, entity.Description, entity.DueDate);
                    await _messagePublisher.PublishAsync(workItemCreatedEvent, "workitem.created");
                }
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackAsync();
                throw new Exception("Erro ao criar WorkItem. Operação revertida.");
            }
        }

        public async Task<WorkItem> GetByIdAsync(Guid id)
        {
            var workItem = await _unitOfWork.WorkItemRepository.GetByIdAsync(id);
            if (workItem == null)
                throw new WorkItemNotFoundException(id);

            return workItem;
        }

        public async Task<IEnumerable<WorkItem>> GetAllAsync()
        {
            return await _unitOfWork.WorkItemRepository.ListAsync();
        }

        public async Task UpdateAsync(WorkItem entity)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                await _unitOfWork.WorkItemRepository.UpdateAsync(entity);

                if (await EnsureTransactionCommitted())
                {
                    var workItemUpdatedEvent = new WorkItemUpdatedEvent(entity.Id, entity.Title, entity.Description, entity.DueDate, entity.Status);
                    await _messagePublisher.PublishAsync(workItemUpdatedEvent, "workitem.updated");
                }

            }
            catch (Exception)
            {
                await _unitOfWork.RollbackAsync();
                throw new Exception("Erro ao atualizar WorkItem. Operação revertida.");
            }
        }

        public async Task DeleteAsync(WorkItem entity)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                await _unitOfWork.WorkItemRepository.DeleteAsync(entity.Id);
                await _unitOfWork.CommitAsync();
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackAsync();
                throw new Exception("Erro ao deletar WorkItem. Operação revertida.");
            }
        }

        public async Task MarkAsCompletedAsync(Guid id)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                var workItem = await GetByIdAsync(id);

                if (workItem.Status == WorkItemStatus.Completed)
                    throw new WorkItemValidationException("WorkItem já está concluído.");

                workItem.MarkAsCompletedAsync();
                if (await EnsureTransactionCommitted())
                {
                    var workItemCompletedEvent = new WorkItemCompletedEvent(workItem.Id);
                    await _messagePublisher.PublishAsync(workItemCompletedEvent, "workitem.completed");
                }
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackAsync();
                throw new Exception("Erro ao marcar WorkItem como concluído. Operação revertida.");
            }
        }

        /// <summary>
        /// Método privado para garantir que a transação foi commitada antes de publicar eventos.
        /// </summary>
        private async Task<bool> EnsureTransactionCommitted()
        {
            var result = await _unitOfWork.CommitAsync();
            return result > 0;
        }
    }
}
