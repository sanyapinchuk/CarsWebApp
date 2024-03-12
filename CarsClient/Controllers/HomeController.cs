using CarsClient.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CarsClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("contact_us")]
        public IActionResult Contact_us()
        {
            ViewData["mailAddress"] = _configuration["mailAddress"];
            return View();
        }
        
        [Route("aboutus")]
        public IActionResult AboutUs()
        {
            ViewData["mailAddress"] = _configuration["mailAddress"];
            return View();
        }

    }
}