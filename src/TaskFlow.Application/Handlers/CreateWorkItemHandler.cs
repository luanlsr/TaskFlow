using AutoMapper;
using FluentValidation;
using MediatR;
using TaskFlow.Application.DTOs;
using TaskFlow.Application.UseCases.Commands;
using TaskFlow.Domain.Core.Interfaces;
using TaskFlow.Domain.Entities;
using TaskFlow.Domain.Events;
using TaskFlow.Domain.Exceptions;
using TaskFlow.Domain.Interfaces.Service;
using TaskFlow.Infrastructure.Messaging;

namespace TaskFlow.Application.Handlers
{
    public class CreateWorkItemHandler : IRequestHandler<CreateWorkItemCommand, WorkItemDTO>
    {
        private readonly IWorkItemService _workItemService;
        private readonly IMapper _mapper;
        private readonly IValidator<WorkItemDTO> _validator;

        public CreateWorkItemHandler(IMapper mapper, IValidator<WorkItemDTO> validator, IWorkItemService workItemService)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _workItemService = workItemService ?? throw new ArgumentNullException(nameof(workItemService));
        }

        public async Task<WorkItemDTO> Handle(CreateWorkItemCommand request, CancellationToken cancellationToken)
        {
            var dto = _mapper.Map<WorkItemDTO>(request);
            var validationResult = await _validator.ValidateAsync(dto, cancellationToken);

            if (!validationResult.IsValid)
            {
                var errors = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new WorkItemValidationException($"Erro(s) de validação: {errors}");
            }

            // Envia para o serviço de domínio
            var workItem = _mapper.Map<WorkItem>(dto);
            await _workItemService.AddAsync(workItem);

            return _mapper.Map<WorkItemDTO>(workItem);
        }
    }
}
