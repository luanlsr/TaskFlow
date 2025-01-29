using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using TaskFlow.Infrastructure.Data.EntityFramework.Context;

namespace TaskFlow.Api.Extensions
{
    public static class EntityFrameworkExtension
    {
        public static IServiceCollection AddEntityFramework(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(connectionString, opt => opt.CommandTimeout((int)TimeSpan.FromMinutes(3).TotalSeconds));
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                options.EnableSensitiveDataLogging();
            });
            return services;
        }
    }
}
