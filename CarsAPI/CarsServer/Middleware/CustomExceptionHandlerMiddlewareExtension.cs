using System.Runtime.CompilerServices;

namespace CarsServer.Middleware
{
    public static class CustomExceptionHandlerMiddlewareExtension
    {
        public static IApplicationBuilder UserCustomExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionHandlerMiddleware>();
        }
    }
}
