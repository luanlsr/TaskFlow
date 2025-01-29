using Microsoft.EntityFrameworkCore;
using Serilog;

namespace TaskFlow.Api.Extensions
{
    public static class SerilogExtension
    {
        public static WebApplicationBuilder AddSerilogConfig(this WebApplicationBuilder builder)
        {
            builder.Host.UseSerilog((context, loggerConfig) =>
            {
                loggerConfig
                    .ReadFrom.Configuration(context.Configuration);
            });
            return builder;
        }
    }
}
