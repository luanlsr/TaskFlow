namespace TaskFlow.Domain.Core.Interfaces
{
    public interface IEventHandler<TEvent>
    {
        Task HandleAsync(TEvent @event);
    }
}
