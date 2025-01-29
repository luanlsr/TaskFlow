using System;
using TaskFlow.CrossCutting.Logging.Interfaces;
using TaskFlow.Domain.Core;

namespace TaskFlow.CrossCutting.Logging
{
    /// <summary>
    /// Serviço de logging que delega a lógica para a instância de ILoggerService injetada.
    /// Útil se quiser padronizar mensagens ou executar ações adicionais antes/depois de logar.
    /// </summary>
    public class LoggerService : ILoggerService
    {
        private readonly ILoggerService _logger;

        public LoggerService(ILoggerService logger)
        {
            _logger = logger;
        }

        public void Information(string message)
        {
            // Aqui, você poderia adicionar formatação adicional de mensagem, se quisesse
            _logger.Information(message);
        }

        public void Warning(string message)
        {
            _logger.Warning(message);
        }

        public void Error(string message, Exception ex = null)
        {
            _logger.Error(message, ex);
        }

        public void Debug(string message)
        {
            _logger.Debug(message);
        }
    }
}
