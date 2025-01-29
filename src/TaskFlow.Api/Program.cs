using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using Serilog;
using System;
using System.Configuration;
using TaskFlow.Api.Extensions;
using TaskFlow.CrossCutting.IoC;
using TaskFlow.CrossCutting.IoC.Mapping;
using TaskFlow.Domain.Core.Interfaces;
using TaskFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using TaskFlow.Infrastructure.Data.EntityFramework.Context;
using FluentValidation.AspNetCore;
using TaskFlow.Application.Validators;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthConfiguration(builder.Configuration);
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console() // Log no console
    .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day) // Log em arquivo
    .CreateLogger();

builder.AddSerilogConfig();
builder.Services.AddEntityFramework(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddValidatorsFromAssemblyContaining<WorkItemDTOValidator>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerDoc();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddAutoMapper(cfg =>
{
}, typeof(WorkItemProfile).Assembly);


builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

builder.Services.AddRabbitMQ(builder.Configuration);


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<TaskFlow.Api.Middlewares.ExceptionMiddleware>();
app.UseSwaggerDoc();
app.UseHttpsRedirection();
app.UseAuthConfiguration();
app.MapControllers();
try
{
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application start-up failed");
}
finally
{
    // Garante que qualquer log pendente seja enviado antes de encerrar
    Log.CloseAndFlush();
}

public partial class Program { }