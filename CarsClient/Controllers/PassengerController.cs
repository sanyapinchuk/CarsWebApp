using CarsClient.Helpers;
using CarsClient.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

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
                ViewData["Postfix"] = _globalVariables.Postfix;
				cars?.Cars?.OrderBy(x => x.ModelName);

				return View(cars?.Cars);
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
                var carApi = await response.Content.ReadFromJsonAsync<CarFullInfoApi>();
                if(carApi == null) {
                    return BadRequest();
                }
                var car = new CarFullInfoDto()
                {
                    Id = carApi.Id,
                    CarType = carApi.CarType,
                    Description = carApi.Description,
                    Images = carApi.Images,
                    ModelName = carApi.ModelName,
                    Price = carApi.Price,
                    ProductionYear = carApi.ProductionYear,
                    SameCars = carApi.SameCars,
                    Categories = new()
                };

                var tags = CarHelper.GetCarStyleTags(car.Images.Count, "");
                ViewData["carStyles0"] = tags[0];
                ViewData["carStyles1"] = tags[1];
                ViewData["carStyles2"] = tags[2];
                ViewData["carStyles3"] = tags[3];
                var titleImage = car.Images.Where(i => i.IsMainImage).First();
                car.Images.Remove(titleImage);
                car.Images.Insert(0, titleImage);

                var props = carApi.Properties.GroupBy(x => new { x.Category, x.Priority })
                    .OrderBy(x => x.Key.Priority);
                foreach (var category in props)
                {
                    var propCategory = new PropertyCategories();
                    propCategory.Properties = new();
                    propCategory.Priority = category.Key.Priority;
                    propCategory.Category = category.Key.Category;

                    foreach (var oneProp in category)
                    {
                        propCategory.Properties.Add(new()
                        {
                            IsKeyProperty = oneProp.IsKeyProperty,
                            Property = oneProp.Property,
                            Value = oneProp.Value
                        });
                    }
                    car.Categories.Add(propCategory);
                }

                ViewData["mailAddress"] = _configuration["mailAddress"];
				ViewData["Postfix"] = _globalVariables.Postfix;
				ViewData["CarPageAddress"] = $"{_globalVariables.AppAddress}car/{car.Id}";
				return View(car);
            }
            else
                return StatusCode((int)response.StatusCode);

        }
    }
}
