using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Domain.Core.Exceptions;

namespace TaskFlow.Domain.Exceptions
{
    public class WorkItemValidationException : DomainException
    {
        public WorkItemValidationException(string message)
            : base($"Erro de validação: {message}") { }
    }
}
