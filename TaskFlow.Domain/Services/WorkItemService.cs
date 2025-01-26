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
        private readonly IWorkItemRepository _repository;

        public WorkItemService(IWorkItemRepository repository)
        {
            _repository = repository;
        }

        public async Task<WorkItem> GetByIdAsync(Guid id)
        {
            var item = await _repository.GetByIdAsync(id);
            if (item == null)
            {
                // Exemplo de exceção, se desejar
                throw new WorkItemNotFoundException(id);
            }
            return item;
        }

        public async Task<IEnumerable<WorkItem>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task AddAsync(WorkItem entity)
        {
            // Exemplo de qualquer lógica de validação antes de inserir
            await ValidateAsync(entity);

            // Ajuste de data, se necessário
            entity.UpdatedAt = DateTime.Now;

            await _repository.AddAsync(entity);
        }

        public async Task UpdateAsync(WorkItem entity)
        {
            // Exemplo de qualquer lógica de validação antes de atualizar
            await ValidateAsync(entity);

            entity.UpdatedAt = DateTime.Now;

            await _repository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            // Você pode querer verificar se existe antes de deletar
            var exists = await _repository.ExistsAsync(id);
            if (!exists)
            {
                throw new WorkItemNotFoundException(id);
            }
            await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<WorkItem>> FindAsync(Func<WorkItem, bool> predicate)
        {
            // Atenção: se estiver usando NHibernate/EF, esse predicate será executado em memória.
            // Uma alternativa seria expor Expressions, mas depende do design do repositório.
            return await _repository.FindAsync(predicate);
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _repository.ExistsAsync(id);
        }

        public async Task ValidateAsync(WorkItem entity)
        {
            // Aqui, você pode colocar qualquer regra de negócio que 
            // precise ser verificada antes de persistir. Ex.:
            if (string.IsNullOrWhiteSpace(entity.Title))
                throw new InvalidWorkItemOperationException("Title cannot be empty.");

            // Exemplo de validação que deve ser async? Ajuste conforme necessidade
            await Task.CompletedTask;
        }

        // Exemplo de método de domínio específico da IWorkItemService
        public async Task MarkAsCompletedAsync(Guid id)
        {
            var item = await GetByIdAsync(id);
            item.MarkAsCompletedAsync(); // Supondo que WorkItem tenha esse método
            await UpdateAsync(item);
        }
    }
}
