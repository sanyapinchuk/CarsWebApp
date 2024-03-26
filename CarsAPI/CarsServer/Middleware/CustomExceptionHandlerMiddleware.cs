using Applicaton.Common.Exceptions;
using System.Net;

namespace CarsServer.Middleware
{
    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomExceptionHandlerMiddleware> _logger;

        public CustomExceptionHandlerMiddleware(RequestDelegate next, ILogger<CustomExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
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

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            HttpStatusCode code;
            switch (ex)
            {
                case BadRequestException:
                    code = HttpStatusCode.BadRequest;
                    _logger.LogWarning($"400 Client Error: {ex.Message}, StackTrace: {ex.StackTrace} ");
                    break;
                case UnauthorizedException:
                    code = HttpStatusCode.Unauthorized;
                    _logger.LogError($"401 Client Error: {ex.Message}, StackTrace: {ex.StackTrace} ");
                    break;
                case EntityNotFoundException:
                    code = HttpStatusCode.NotFound;
                    _logger.LogError($"400 Not found exception: {ex.Message}, StackTrace: {ex.StackTrace}");
                    break;

                default:
                    _logger.LogError($"500 Internal error: {ex.Message}, StackTrace: {ex.StackTrace}");
                    code = HttpStatusCode.InternalServerError;
                    break;
            }
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            string message;
            var environment = context.RequestServices.GetRequiredService<IWebHostEnvironment>();

            message = environment.IsProduction() 
                ? $"Error: {ex.Message}, Status: {(int)code}" 
                : $"Error: {ex.Message}, Status: {(int)code}, StackTrace: {ex.StackTrace}";

            return context.Response.WriteAsync(message);
        }
    }
}