namespace TaskFlow.Domain.Core.Events
{
    public interface IDomainEvent
    {
        DateTime OccurredOn { get; }
    }
}
