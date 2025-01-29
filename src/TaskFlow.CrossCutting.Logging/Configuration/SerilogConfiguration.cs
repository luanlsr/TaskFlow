using Serilog;
using Serilog.Events;
using Serilog.Sinks.Elasticsearch;
using System;

namespace TaskFlow.CrossCutting.Logging.Configuration
{
    public static class SerilogConfiguration
    {
        public static void ConfigureLogger(string elasticsearchUri, string indexFormat = "taskflow-logs-{0:yyyy.MM.dd}")
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.Console() // Para logs no console
                .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(elasticsearchUri))
                {
                    AutoRegisterTemplate = true, // Cria automaticamente o template de índice no Elasticsearch
                    IndexFormat = string.Format(indexFormat, DateTime.UtcNow),
                    FailureCallback = (logEvent, ex) => 
                    {
                        Console.WriteLine("Falha ao enviar log para o Elasticsearch: " + ex.Message);
                        if (logEvent != null)
                        {
                            Console.WriteLine("Detalhes do LogEvent: " + logEvent.RenderMessage());
                        }
                    },
                    EmitEventFailure = EmitEventFailureHandling.WriteToSelfLog | EmitEventFailureHandling.WriteToFailureSink | EmitEventFailureHandling.RaiseCallback
                })
                .CreateLogger();
        }

        public static void CloseAndFlushLogger()
        {
            Log.CloseAndFlush();
        }
    }
}
