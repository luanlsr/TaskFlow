using AutoMapper;
using MediatR;
using TaskFlow.Application.DTOs;
using TaskFlow.Application.UseCases.Commands;
using TaskFlow.Domain.Core.Interfaces;
using TaskFlow.Domain.Entities;

public class UpdateWorkItemHandler : IRequestHandler<UpdateWorkItemCommand, WorkItemDTO>
{
    private readonly IRepository<WorkItem, Guid> _repository;
    private readonly IMapper _mapper;

    public UpdateWorkItemHandler(IRepository<WorkItem, Guid> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<WorkItemDTO> Handle(UpdateWorkItemCommand request, CancellationToken cancellationToken)
    {
        var item = await _repository.GetByIdAsync(request.Id);
        if (item == null) throw new Exception($"WorkItem {request.Id} not found.");

        // Podemos mapear *parcialmente* se as propriedades tiverem set public,
        // ou chamar método de domínio: item.UpdateWorkItem(request.Title, request.Description, ...)

        // Exemplo: uso parcial do AutoMapper + método de domínio
        _mapper.Map(request, item);
        // Se 'Title' / 'Description' tiverem 'private set;',
        // isso não funcionará sem trocar para public set, 
        // ou sem customizar 'AfterMap' chamando "item.UpdateWorkItem(...)" dentro do perfil.

        await _repository.UpdateAsync(item);

        return _mapper.Map<WorkItemDTO>(item);
    }
}
