using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskFlow.Application.UseCases.Commands;
using TaskFlow.CrossCutting.Logging;
using TaskFlow.CrossCutting.Logging.Interfaces;
using TaskFlow.Domain.Core.Interfaces;
using TaskFlow.Domain.Interfaces.Repository;
using TaskFlow.Domain.Interfaces.Service;
using TaskFlow.Domain.Services;
using TaskFlow.Infrastructure.Data.EntityFramework;
using TaskFlow.Infrastructure.Data.EntityFramework.Context;
using TaskFlow.Infrastructure.Data.EntityFramework.Repositories;

namespace TaskFlow.CrossCutting.IoC
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<ISerilogLoggerService, SerilogLogger>();
            services.AddSingleton<IConsoleLoggerService, ConsoleLogger>();

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateWorkItemCommand).Assembly));
            // WorkItem
            services.AddScoped<IWorkItemRepository, WorkItemRepository>();
            services.AddScoped<IWorkItemService, WorkItemService>();

            // Loggers
            services.AddSingleton<ISerilogLoggerService, SerilogLogger>();
            services.AddSingleton<IConsoleLoggerService, ConsoleLogger>();

            // Registro do repositório genérico
            services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
            services.AddScoped<IDbFactory, DbFactory>();

            return services;

        }
    }
}
