using CarsClient.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarsClient.Controllers
{
    public class ErrorController : Controller
    {
        private readonly ILogger<ErrorController> _logger;
        public ErrorController(ILogger<ErrorController> logger)
        {
            _logger= logger;
        }

        [HttpGet("/error")]
        public IActionResult Error(int statusCode = 500)
        {
            
            _logger.LogWarning($"Handle http error, status code= {statusCode}");

            var errorModel = new ErrorViewModel();
            string message;
            errorModel.Code = statusCode;
            switch (statusCode)
            {
                case 400:
                    message = "Проверьте корректность запроса";
                    break;
                case 403:
                    message = "Доступ запрещён!";
                    break;
                case 404:
                    message = "Ресурс не найден!";
                    break;
                case 405:
                    message = "Метод Htpp не разрешен";
                    break;
                case 500:
                    message = "Ошибка сервера";
                    break;
                default:
                    message = "Неизвестная ошибка, попробуйте позже";
                    break;
            }
            errorModel.Description = message;

            return View(errorModel);
        }

    }
    
}
