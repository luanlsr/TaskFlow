using TaskFlow.Domain.Core.Interfaces;
using TaskFlow.Infrastructure.Messaging.RabbitMQ.Consumer;
using TaskFlow.Infrastructure.Messaging.RabbitMQ.Publisher;

namespace TaskFlow.Api.Extensions
{
    public static class RabbitMQExtensions
    {
        public static IServiceCollection AddRabbitMQ(this IServiceCollection services, IConfiguration configuration)
        {
            var rabbitMQSettings = configuration.GetSection("RabbitMQ");

            var hostName = rabbitMQSettings["HostName"];
            var userName = rabbitMQSettings["UserName"];
            var password = rabbitMQSettings["Password"];

            // Configura o publisher
            services.AddSingleton<IMessagePublisher>(sp =>
                new RabbitMQPublisher(hostName, userName, password));

            // Configura o consumer
            services.AddSingleton(sp =>
                new RabbitMQConsumer(hostName, userName, password));

            return services;
        }
    }
}
