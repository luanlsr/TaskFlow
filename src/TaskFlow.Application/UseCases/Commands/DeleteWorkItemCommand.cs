using MediatR;

namespace TaskFlow.Application.UseCases.Commands
{
    public class DeleteWorkItemCommand : IRequest<Unit>
    {
        public Guid Id { get; }

        public DeleteWorkItemCommand(Guid id)
        {
            Id = id;
        }
    }
}
