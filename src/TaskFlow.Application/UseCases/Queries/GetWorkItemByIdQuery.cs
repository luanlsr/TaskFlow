using MediatR;
using TaskFlow.Application.DTOs;
using System;

namespace TaskFlow.Application.UseCases.Queries
{
    public class GetWorkItemByIdQuery : IRequest<WorkItemDTO>
    {
        public Guid Id { get; }

        public GetWorkItemByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
