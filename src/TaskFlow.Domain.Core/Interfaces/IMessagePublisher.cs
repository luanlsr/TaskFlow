using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskFlow.Domain.Core.Interfaces
{
    public interface IMessagePublisher
    {
        Task PublishAsync<T>(T @event, string queueName);
    }
}
