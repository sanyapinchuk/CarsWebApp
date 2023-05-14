using CarsClient.Helpers;
using CarsClient.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace CarsClient.Controllers
{
    public class PassengerController : Controller
    {
        [Route("passenger")]
        public async Task<IActionResult> AllCars()
        {
            var response = await GlobalVariables.WebApiClient.GetAsync("car/getall");
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
            var response = await GlobalVariables.WebApiClient.GetAsync($"car/get/{id}");
            if (response.IsSuccessStatusCode)
            {
                var car = await response.Content.ReadFromJsonAsync<CarFullInfo>();
                // ViewData["apiEditUrl"] = GlobalVariables.WebApiClient.BaseAddress + "Contact/edit";
                var tags = CarHelper.GetCarStyleTags(car.Images.Count, "");
                ViewData["carStyles"] = tags;
				return View(car);
            }
            else
                return StatusCode((int)response.StatusCode);

        }
    }
}
