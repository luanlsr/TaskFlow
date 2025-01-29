
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TaskFlow.Infrastructure.Messaging.RabbitMQ.Consumer
{
    public class RabbitMQConsumer
    {
        private readonly ConnectionFactory _connectionFactory;

        public RabbitMQConsumer(string hostName, string userName, string password)
        {
            _connectionFactory = new ConnectionFactory
            {
                HostName = hostName,
                UserName = userName,
                Password = password
            };
        }

        public void Consume<T>(string queueName, Func<T, Task> onMessageReceived)
        {
            var connection = _connectionFactory.CreateConnection(); // Cria a conexão
            var channel = connection.CreateModel(); // Cria o canal

            channel.QueueDeclare(queue: queueName,
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var consumer = new EventingBasicConsumer(channel); // Cria o consumidor
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var @event = JsonSerializer.Deserialize<T>(message);

                if (@event != null)
                {
                    await onMessageReceived(@event); // Processa a mensagem
                }
            };

            channel.BasicConsume(queue: queueName,
                                 autoAck: true,
                                 consumer: consumer);
        }
    }
}
