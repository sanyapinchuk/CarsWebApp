using CarsClient.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CarsClient.Controllers
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

    }
}