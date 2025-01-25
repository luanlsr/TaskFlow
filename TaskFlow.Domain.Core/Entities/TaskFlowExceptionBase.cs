public abstract class TaskFlowException : Exception
{
    public string ErrorCode { get; }

    protected TaskFlowException(string message, string errorCode = null)
        : base(message)
    {
        ErrorCode = errorCode;
    }

    protected TaskFlowException(string message, Exception innerException, string errorCode = null)
        : base(message, innerException)
    {
        ErrorCode = errorCode;
    }
}