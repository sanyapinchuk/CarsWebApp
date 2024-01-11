using CarsClient.Helpers;
using CarsClient.Models.Dto;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.CodeAnalysis.Operations;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;

namespace CarsClient.Controllers
{
    [Route("admin_8rm7yxmfos9o3bkk3f4he67jn7")]
	//[Authorize]
	public class AdminController : Controller
    {
        private GlobalVariables _globalVariables;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public AdminController(
            GlobalVariables globalVariables,
            IWebHostEnvironment hostingEnvironment)
        {
            _globalVariables= globalVariables;
            _hostingEnvironment= hostingEnvironment;
        }

		[HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return RedirectToAction("AllCars");
        }

		[HttpGet]
        [Route("passenger")]
        public async Task<IActionResult> AllCars()
        {
			var response3 = await _globalVariables.WebApiClient.GetAsync("car/getall");
			if (response3.IsSuccessStatusCode)
			{
				var cars = await response3.Content.ReadFromJsonAsync<CarList>();
				// ViewData["apiEditUrl"] = GlobalVariables.WebApiClient.BaseAddress + "Contact/edit";
				ViewData["Postfix"] = _globalVariables.Postfix;
                cars?.Cars?.OrderBy(x => x.ModelName);

				return View(cars?.Cars);
			}
			else
            { 
				return StatusCode((int)response3.StatusCode);
			}
        }
		[Route("car/{id}")]
		public async Task<IActionResult> GetCar(Guid id)
		{
			var response = await _globalVariables.WebApiClient.GetAsync($"car/get/{id}");
			if (response.IsSuccessStatusCode)
			{
				var carApi = await response.Content.ReadFromJsonAsync<CarFullInfoApi>();

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

				var tags = CarHelper.GetCarStyleTags(car.Images.Count,"../");
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
				ViewData["Postfix"] = _globalVariables.Postfix;
				ViewData["CarPageAddress"] = $"{_globalVariables.AppAddress}car/{car.Id}";
				return View(car);
			}
			else
				return StatusCode((int)response.StatusCode);
		}


        [HttpDelete("{id}")]
        [Route("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _globalVariables.WebApiClient.DeleteAsync($"car/delete/{id}");
            if (result.IsSuccessStatusCode)
            {
                return Ok();
            }
            else
            {
                if (result.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return StatusCode(401);
                }
                return StatusCode((int)result.StatusCode);
            }

		}

        [HttpGet]
        [Route("create")]
        public async Task<IActionResult> Create()
        {
			ViewData["Postfix"] = _globalVariables.Postfix;
			return View();
        }

        // POST: AdminController/Create
        [HttpPost]
        [Route("create")]
        public async  Task<IActionResult> Create(IFormCollection form)
        {
            var model = form["model"];
            var price = form["price"];
            var color = form["color"];
            var description = form["description"];
            var cartype = form["cartype"];
            var productionYear = form["productionYear"];
            var mainImage = form["mainImage"];


            var props = form.Where(f => Regex.IsMatch(f.Key, @"^prop\d+$"));
            var vals = form.Where(f => f.Key.StartsWith("val"));
            var checks = form.Where(f => f.Key.StartsWith("check")); 
            var propsCategory = form.Where(f => Regex.IsMatch(f.Key, @"^propCategory\d+$"));
            var propsCategoryPriorities = form.Where(f => Regex.IsMatch(f.Key, @"^propCategoryPriority\d+$"));


            var propsList = new List<PropertyFullInfoApi>();
            foreach(var prop in props)
            {
                var number = int.Parse(prop.Key.Substring(4));
                var propName = prop.Value;
                var value = vals.Where(v => v.Key == $"val{number}")?.FirstOrDefault().Value ?? "";
                var isMainProp = checks.Count(v => v.Key == $"check{number}") > 0;
                var category = propsCategory.Where(v => v.Key == $"propCategory{number}")?.FirstOrDefault().Value ?? "";
                var categoryPriority = propsCategoryPriorities.Where(v => v.Key == $"propCategoryPriority{number}")?.FirstOrDefault().Value ?? "";
                if(!int.TryParse(categoryPriority, out var categoryPriorityValue))
                {
                    categoryPriorityValue = 0;
                }

                propsList.Add(new PropertyFullInfoApi()
                {
                    Property = propName,
                    Value = value,
                    IsKeyProperty = isMainProp,
                    Category = category,
                    Priority = categoryPriorityValue
                });
            }

            var images = new List<ImageInfo>();
            var imagesInputs = form.Where(f => f.Key.StartsWith("photo"));
            foreach (var image in imagesInputs)
            {
                var path = image.Value;
                if (!string.IsNullOrWhiteSpace(path))
                {
					images.Add(new ImageInfo()
					{
						Path = path,
						IsMainImage = image.Key == mainImage
					});
				}
            }
            if (images.Where(i => i.IsMainImage).Count() == 0)
            {
                var first = images.FirstOrDefault();
                if (first != null)
                    first.IsMainImage = true;
            }

            var car = new CarFullInfoApi()
            {
                Price = int.Parse(price),
                CarType = cartype,
                Description = description,
                Images = images,
                ModelName = model,
                ProductionYear = int.Parse(productionYear),
                Properties = propsList
            };

            var response = await _globalVariables.WebApiClient.PostAsJsonAsync($"car/create",
                new { CreateCarDto = car });

            if(!response.IsSuccessStatusCode)
                return StatusCode((int)response.StatusCode);

            
            return RedirectToAction("AllCars");
        }

        [HttpGet]
        [Route("update/{id}")]
        public async Task<IActionResult> Update(Guid id)
        {
            var response = await _globalVariables.WebApiClient.GetAsync($"car/get/{id}");
            if (response.IsSuccessStatusCode)
            {
                var car = await response.Content.ReadFromJsonAsync<CarFullInfoApi>();
				ViewData["Postfix"] = _globalVariables.Postfix;

				return View(car);
            }
            else
                return StatusCode((int)response.StatusCode);
        }

        [HttpPost]
        [Route("update")]
        public async Task<IActionResult> Update(IFormCollection form)
		{
            var id = form["id"];
			var model = form["model"];
			var price = form["price"];
			var color = form["color"];
			var description = form["description"];
			var cartype = form["cartype"];
			var productionYear = form["productionYear"];
			var mainImage = form["mainImage"];


			var props = form.Where(f => Regex.IsMatch(f.Key, @"^prop\d+$"));
			var vals = form.Where(f => f.Key.StartsWith("val"));
			var checks = form.Where(f => f.Key.StartsWith("check"));
            var propsCategory = form.Where(f => Regex.IsMatch(f.Key, @"^propCategory\d+$"));
			var propsCategoryPriorities = form.Where(f => Regex.IsMatch(f.Key, @"^propCategoryPriority\d+$"));

			var propsList = new List<PropertyFullInfoApi>();
			foreach (var prop in props)
			{
				var number = int.Parse(prop.Key.Substring(4));
				var propName = prop.Value;
				var value = vals.Where(v => v.Key == $"val{number}")?.FirstOrDefault().Value ?? "";
				var isMainProp = checks.Count(v => v.Key == $"check{number}") > 0;
                var category = propsCategory.Where(v => v.Key == $"propCategory{number}")?.FirstOrDefault().Value ?? "";
                var categoryPriority = propsCategoryPriorities.Where(v => v.Key == $"propCategoryPriority{number}")?.FirstOrDefault().Value ?? "";
                if (!int.TryParse(categoryPriority, out var categoryPriorityValue))
                {
                    categoryPriorityValue = 0;
                }

                propsList.Add(new PropertyFullInfoApi()
				{
					Property = propName,
					Value = value,
					IsKeyProperty = isMainProp,
                    Category = category,
                    Priority = categoryPriorityValue
                });
			}

            var images = new List<ImageInfo>();
            var imagesInputs = form.Where(f => f.Key.StartsWith("photo"));
            foreach (var image in imagesInputs)
            {
                var path = image.Value;

                if (!string.IsNullOrWhiteSpace(path))
                {
					images.Add(new ImageInfo()
					{
						Path = path,
						IsMainImage = image.Key == mainImage
					});
				}
            }
            if (images.Where(i => i.IsMainImage).Count() == 0)
            {
                var first = images.FirstOrDefault();
                if (first != null)
                    first.IsMainImage = true;
            }

			var car = new CarFullInfoApi()
			{
				Price = int.Parse(price),
				CarType = cartype,
				Description = description,
				Images = images,
				ModelName = model,
				ProductionYear = int.Parse(productionYear),
				Properties = propsList
			};

			var response = await _globalVariables.WebApiClient.PutAsJsonAsync($"car/update",
				new { Id = Guid.Parse(id), CarInfo = car });

			if (!response.IsSuccessStatusCode)
				return StatusCode((int)response.StatusCode);


			return RedirectToAction("AllCars");
		}

        public IActionResult Logout()
        {
            CarHelper.Logout(HttpContext);
            return RedirectToAction("Index", "Home");
		}

	}
}
