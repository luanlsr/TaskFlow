using AutoMapper;
using FluentValidation;
using MediatR;
using TaskFlow.Application.DTOs;
using TaskFlow.Application.UseCases.Commands;
using TaskFlow.Domain.Core.Interfaces;
using TaskFlow.Domain.Entities;
using TaskFlow.Domain.Exceptions;
using TaskFlow.Domain.Interfaces.Service;

namespace TaskFlow.Application.Handlers
{
    public class UpdateWorkItemHandler : IRequestHandler<UpdateWorkItemCommand, WorkItemDTO>
    {
        private readonly IWorkItemService _workItemService;
        private readonly IMapper _mapper;
        private readonly IValidator<WorkItemDTO> _validator;

        public UpdateWorkItemHandler(
            IMapper mapper,
            IValidator<WorkItemDTO> validator,
            IWorkItemService service)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _workItemService = service ?? throw new ArgumentNullException(nameof(service));
        }

        public async Task<WorkItemDTO> Handle(UpdateWorkItemCommand request, CancellationToken cancellationToken)
        {
            // Tenta buscar o WorkItem no repositório
            var workItem = await _workItemService.GetByIdAsync(request.Id);
            if (workItem == null)
            {
                throw new WorkItemNotFoundException(request.Id); // Exceção personalizada
            }

            // Mapeia o comando para o DTO
            var dto = _mapper.Map<WorkItemDTO>(request);

            // Valida o DTO
            var validationResult = await _validator.ValidateAsync(dto, cancellationToken);
            if (!validationResult.IsValid)
            {
                var errors = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new WorkItemValidationException($"Erro(s) de validação: {errors}");
            }

            // Atualiza os dados no objeto de domínio
            _mapper.Map(request, workItem);

            // Atualiza no repositório
            await _workItemService.UpdateAsync(workItem);

            // Retorna o WorkItem atualizado como DTO
            return _mapper.Map<WorkItemDTO>(workItem);
        }
    }
}
