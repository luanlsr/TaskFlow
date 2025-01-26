using Serilog;
using TaskFlow.Api.Extensions;
using TaskFlow.CrossCutting.IoC;
using TaskFlow.CrossCutting.IoC.Mapping;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, loggerConfig) =>
{
    loggerConfig
        .ReadFrom.Configuration(context.Configuration);
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerDoc();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddAutoMapper(cfg =>
{
}, typeof(WorkItemProfile).Assembly);


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSwaggerDoc();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
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
