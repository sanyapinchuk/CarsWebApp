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
                    break;
                    
                case EntityNotFoundException:
                    code = HttpStatusCode.NotFound; 
                    break;

                default:
                    _logger.LogError(ex.Message);
                    code = HttpStatusCode.InternalServerError;
                    break;
            }
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync("Error: " + ex.Message);
        }
    }
}