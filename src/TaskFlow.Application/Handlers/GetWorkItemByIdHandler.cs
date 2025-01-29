using MediatR;
using TaskFlow.Application.DTOs;
using TaskFlow.Application.UseCases.Queries;
using TaskFlow.Domain.Core.Interfaces;
using TaskFlow.Domain.Entities;
using TaskFlow.Domain.Exceptions;
using AutoMapper;
using TaskFlow.Domain.Interfaces.Service;

namespace TaskFlow.Application.Handlers
{
    public class GetWorkItemByIdHandler : IRequestHandler<GetWorkItemByIdQuery, WorkItemDTO>
    {
        private readonly IWorkItemService _workItemService;
        private readonly IMapper _mapper;

        public GetWorkItemByIdHandler(IWorkItemService service, IMapper mapper)
        {
            _workItemService = service ?? throw new ArgumentNullException(nameof(service));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<WorkItemDTO> Handle(GetWorkItemByIdQuery request, CancellationToken cancellationToken)
        {
            var workItem = await _workItemService.GetByIdAsync(request.Id);
            if (workItem == null)
            {
                throw new WorkItemNotFoundException(request.Id);
            }

            return _mapper.Map<WorkItemDTO>(workItem);
        }
    }
}
