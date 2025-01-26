using Microsoft.Extensions.DependencyInjection;
using TaskFlow.CrossCutting.Logging.Interfaces;

namespace TaskFlow.CrossCutting.Logging
{
    public static class LoggingExtensions
    {
        /// <summary>
        /// Registra os serviços de logging no contêiner de IoC.
        /// Ajuste conforme sua necessidade (ex: escolher SerilogLogger ou ConsoleLogger).
        /// </summary>
        public static IServiceCollection AddLoggingServices(this IServiceCollection services)
        {
            // 1) Registra a implementação concreta de ILoggerService. 
            //    Aqui, vamos supor que queremos usar o SerilogLogger por padrão.
            services.AddSingleton<ILoggerService, SerilogLogger>();

            // 2) Registra o LoggerService que encapsula a chamada ao ILoggerService.
            //    Caso não use o LoggerService, pode remover esta linha.
            services.AddSingleton<LoggerService>();

            return services;
        }
    }
}
