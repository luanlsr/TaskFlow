namespace TaskFlow.Domain.Core
{
    /// <summary>
    /// Define os métodos essenciais para logging.
    /// </summary>
    public interface ILoggerService
    {
        void Information(string message);
        void Warning(string message);
        void Error(string message, Exception ex = null);
        void Debug(string message);
    }
}
