using CarsServer.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CarsServer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("commercial")]
        public IActionResult Commercial()
        {
            return View();
        }

        [Route("passenger")]
        public IActionResult Passenger()
        {
            return View();
        }

        [Route("news")]
        public IActionResult News()
        {
            return View();
        }

        [Route("contact_us")]
        public IActionResult Contact_us()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}