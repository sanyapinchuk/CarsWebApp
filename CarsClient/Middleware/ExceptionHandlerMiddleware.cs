using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace CarsClient.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
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
                await Task.Run(() => HadleProgramExceptionAsync(context, ex));
            }
        }
        
        private void HadleProgramExceptionAsync(HttpContext context, Exception exception)
        {
            _logger.LogError($"statusCode = {context.Response.StatusCode}, exeptionMessage = {exception.Message}, trace = {exception.StackTrace}");
            context.Response.Redirect("/Error?statusCode=500");
        }
      
            
        
    }
}
