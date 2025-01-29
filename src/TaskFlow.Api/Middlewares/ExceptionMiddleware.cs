using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;
using TaskFlow.Domain.Core.Exceptions;

namespace TaskFlow.Api.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var statusCode = exception switch
            {
                DomainException => HttpStatusCode.BadRequest,
                _ => HttpStatusCode.InternalServerError
            };

            context.Response.StatusCode = (int)statusCode;

            var result = JsonSerializer.Serialize(new
            {
                error = exception.Message,
                type = exception.GetType().Name,
                stackTrace = exception.StackTrace // Opcional: Remova em produção
            });

            return context.Response.WriteAsync(result);
        }
    }
}
