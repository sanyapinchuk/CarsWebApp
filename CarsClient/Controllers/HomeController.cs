﻿using CarsClient.Models;
using CarsClient.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CarsClient.Controllers
{
    public class HomeController : Controller
    {
        private GlobalVariables _globalVariables;
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, 
            IConfiguration configuration,
            GlobalVariables globalVariables)
        {

            _globalVariables = globalVariables;
            _logger = logger;
            _configuration = configuration;
        }

        [Route("")]
        [Route("index")]
        public async Task<IActionResult> Index()
        {
            var responseConfig = await _globalVariables.WebApiClient.GetAsync("car/FilterConfig");
            if (responseConfig.IsSuccessStatusCode)
            {
                var config = await responseConfig.Content.ReadFromJsonAsync<CarFilterConfig>();
                ViewData["config"] = config;
                return View();
            }
            else
            {
                return StatusCode((int)responseConfig.StatusCode);
            }
        }

        [Route("contact_us")]
        public IActionResult Contact_us()
        {
            ViewData["mailAddress"] = _configuration["mailAddress"];
            return View();
        }

    }
}