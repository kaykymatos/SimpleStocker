using SimpleStocker.InventoryApi.DTO;
using System.Net;
using System.Text.Json;

namespace SimpleStocker.InventoryApi.Middlewares
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
                await _next(context); // continua o pipeline normalmente
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex); // intercepta a exceção
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = new ApiResponse<object>(false, "Erro interno no servicor " + exception.Message, [], new object(), context.Response.StatusCode);

            var json = JsonSerializer.Serialize(response);
            return context.Response.WriteAsync(json);
        }
    }
}
