using TaskFlow.Domain.Core.Exceptions;

public class InvalidWorkItemStatusException : DomainException
{
    public InvalidWorkItemStatusException(string status)
        : base($"O status '{status}' é inválido para um WorkItem.") { }
}