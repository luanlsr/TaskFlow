using Serilog;
using System;
using TaskFlow.CrossCutting.Logging.Interfaces;

namespace TaskFlow.CrossCutting.Logging
{
    public class SerilogLogger : ISerilogLoggerService
    {
        public void Information(string message)
        {
            Log.Information(message);
        }

        public void Warning(string message)
        {
            Log.Warning(message);
        }

        public void Error(string message, Exception ex = null)
        {
            if (ex == null)
                Log.Error(message);
            else
                Log.Error(ex, message);
        }

        public void Debug(string message)
        {
            Log.Debug(message);
        }

        // Aqui você pode adicionar métodos específicos do Serilog, se quiser
    }
}
