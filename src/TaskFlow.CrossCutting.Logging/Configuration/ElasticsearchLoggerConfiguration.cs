using Serilog;
using Serilog.Sinks.Elasticsearch;
using System;

namespace TaskFlow.CrossCutting.Logging.Configuration
{
    public static class ElasticsearchLoggerConfiguration
    {
        public static void ConfigureElasticsearchLogger(string elasticUri)
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(elasticUri))
                {
                    AutoRegisterTemplate = true,
                    IndexFormat = "taskflow-logs-{0:yyyy.MM.dd}"
                })
                .CreateLogger();
        }
    }
}
