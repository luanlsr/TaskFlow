using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Application.DTOs;
using TaskFlow.Domain.Core.Interfaces;
using TaskFlow.Domain.Entities;

namespace TaskFlow.Application.UseCases.Commands
{
    public class CreateWorkItemCommand : IRequest<WorkItemDTO>
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }

    public class CreateWorkItemHandler : IRequestHandler<CreateWorkItemCommand, WorkItemDTO>
    {
        private readonly IRepository<WorkItem, Guid> _repository;

        public CreateWorkItemHandler(IRepository<WorkItem, Guid> repository)
        {
            _repository = repository;
        }

        public async Task<WorkItemDTO> Handle(CreateWorkItemCommand request, CancellationToken cancellationToken)
        {
            var task = new WorkItem
            {
                
            };

            await _repository.AddAsync(task);
            return new WorkItemDTO { Id = task.Id, Title = task.Title, Description = task.Description };
        }
    }

}
