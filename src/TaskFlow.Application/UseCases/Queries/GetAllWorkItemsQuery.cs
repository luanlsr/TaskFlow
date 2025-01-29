using MediatR;
using TaskFlow.Application.DTOs;
using System.Collections.Generic;

namespace TaskFlow.Application.UseCases.Queries
{
    public class GetAllWorkItemsQuery : IRequest<IEnumerable<WorkItemDTO>>
    {
    }
}
