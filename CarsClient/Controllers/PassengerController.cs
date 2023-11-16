﻿using CarsClient.Helpers;
using CarsClient.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace CarsClient.Controllers
{
    public class PassengerController : Controller
    {
        private GlobalVariables _globalVariables;
        private readonly IConfiguration _configuration;
        public PassengerController( GlobalVariables globalVariables,
                IConfiguration configuration)
        {
            _globalVariables = globalVariables;
            _configuration = configuration;
        }


        [Route("passenger")]
        public async Task<IActionResult> AllCars()
        {
            var response = await _globalVariables.WebApiClient.GetAsync("car/getall");
            if (response.IsSuccessStatusCode)
            {
                var cars = await response.Content.ReadFromJsonAsync<CarList>(); 
               // ViewData["apiEditUrl"] = GlobalVariables.WebApiClient.BaseAddress + "Contact/edit";
                return View(cars.Cars);
            }
            else
                return StatusCode((int)response.StatusCode);

        }
        [Route("car/{id}")]
        public async Task<IActionResult> GetCar(Guid id)
        {
            var response = await _globalVariables.WebApiClient.GetAsync($"car/get/{id}");
            if (response.IsSuccessStatusCode)
            {
                var car = await response.Content.ReadFromJsonAsync<CarFullInfo>();
                var tags = CarHelper.GetCarStyleTags(car.Images.Count, "");
                ViewData["carStyles0"] = tags[0];
                ViewData["carStyles1"] = tags[1];
                ViewData["carStyles2"] = tags[2];
                ViewData["carStyles3"] = tags[3];
                var titleImage = car.Images.Where(i => i.IsMainImage).First();
                car.Images.Remove(titleImage);
                car.Images.Insert(0, titleImage);

                ViewData["mailAddress"] = _configuration["mailAddress"];
                return View(car);
            }
            else
                return StatusCode((int)response.StatusCode);

        }
    }
}
