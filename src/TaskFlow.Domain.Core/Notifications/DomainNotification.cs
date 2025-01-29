namespace TaskFlow.Domain.Core.Notifications
{
    public class DomainNotification
    {
        public string Key { get; }
        public string Message { get; }

        public DomainNotification(string key, string message)
        {
            Key = key;
            Message = message;
        }
    }
}
