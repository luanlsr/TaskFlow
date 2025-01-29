using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TaskFlow.Domain.Core.Interfaces;

namespace TaskFlow.Infrastructure.Messaging.RabbitMQ.Publisher
{
    public class RabbitMQPublisher : IMessagePublisher
    {
        private readonly ConnectionFactory _connectionFactory;

        public RabbitMQPublisher(string hostName, string userName, string password)
        {
            _connectionFactory = new ConnectionFactory
            {
                HostName = hostName,
                UserName = userName,
                Password = password
            };
        }

        public async Task PublishAsync<T>(T @event, string queueName)
        {
            using var connection = _connectionFactory.CreateConnection(); // Cria a conexão
            using var channel = connection.CreateModel(); // Cria o canal

            channel.QueueDeclare(queue: queueName,
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var message = JsonSerializer.Serialize(@event);
            var body = Encoding.UTF8.GetBytes(message);

            await Task.Run(() =>
            {
                channel.BasicPublish(exchange: "",
                                     routingKey: queueName,
                                     basicProperties: null,
                                     body: body);
            });
        }
    }
}
