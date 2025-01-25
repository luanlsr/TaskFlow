namespace TaskFlow.Domain.Core.Interfaces
{
    public interface INotifier
    {
        void Notify(string message);
        bool HasNotifications();
        IEnumerable<string> GetNotifications();
    }
}
