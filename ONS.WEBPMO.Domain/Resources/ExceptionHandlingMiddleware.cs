using Microsoft.AspNetCore.Http;
using System.Text.Json;


namespace ONS.WEBPMO.Domain.Resources
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (BusinessValidationException ex)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Response.ContentType = "application/json";
                var response = new { errors = ex.Errors };
                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";
                var response = new { message = "Erro inesperado no servidor." };
                await context.Response.WriteAsync(JsonSerializer.Serialize(response));

                // Log do erro (opcional)
                // _logger.LogError(ex, "Erro inesperado");
            }
        }
    }
}
