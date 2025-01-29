using MediatR;
using TaskFlow.Application.DTOs;
using TaskFlow.Application.UseCases.Queries;
using TaskFlow.Domain.Core.Interfaces;
using TaskFlow.Domain.Entities;
using AutoMapper;
using TaskFlow.Domain.Interfaces.Service;

namespace TaskFlow.Application.Handlers
{
    public class GetAllWorkItemsHandler : IRequestHandler<GetAllWorkItemsQuery, IEnumerable<WorkItemDTO>>
    {
        private readonly IWorkItemService _workItemService;
        private readonly IMapper _mapper;

        public GetAllWorkItemsHandler(IWorkItemService service, IMapper mapper)
        {
            _workItemService = service ?? throw new ArgumentNullException(nameof(service));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<WorkItemDTO>> Handle(GetAllWorkItemsQuery request, CancellationToken cancellationToken)
        {
            var workItems = await _workItemService.GetAllAsync();
            return _mapper.Map<IEnumerable<WorkItemDTO>>(workItems);
        }
    }
}
