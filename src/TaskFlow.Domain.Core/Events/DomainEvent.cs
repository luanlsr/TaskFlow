namespace TaskFlow.Domain.Core.Events
{
    public abstract class DomainEvent : IDomainEvent
    {
        public DateTime OccurredOn { get; private set; }

        protected DomainEvent()
        {
            OccurredOn = DateTime.UtcNow;
        }
    }
}
