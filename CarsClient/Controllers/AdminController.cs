using CarsClient.Helpers;
using CarsClient.Models.Dto;
using CarsClient.Services;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.CodeAnalysis.Operations;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace CarsClient.Controllers
{
    [Route("admin")]
	[Authorize]
	public class AdminController : Controller
    {
        private IIdentityServer4Service _identityServer4Service;
        private GlobalVariables _globalVariables;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public AdminController(IIdentityServer4Service identityServer4Service, 
            GlobalVariables globalVariables,
            IWebHostEnvironment hostingEnvironment)
        {
            _identityServer4Service= identityServer4Service;
            _globalVariables= globalVariables;
            _hostingEnvironment= hostingEnvironment;
        }

		[HttpGet]
        public async Task<IActionResult> Index()
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
				return View(cars.Cars);
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
				var car = await response.Content.ReadFromJsonAsync<CarFullInfo>();
				// ViewData["apiEditUrl"] = GlobalVariables.WebApiClient.BaseAddress + "Contact/edit";
				var tags = CarHelper.GetCarStyleTags(car.Images.Count,"../");
				ViewData["carStyles"] = tags;
				return View(car);
			}
			else
				return StatusCode((int)response.StatusCode);
		}


        [HttpDelete("{id}")]
        [Route("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var OAuth2Token = await _identityServer4Service.GetToken("carsApi.read");

            _globalVariables.WebApiClient.SetBearerToken(OAuth2Token.AccessToken);
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
            return View();
        }

        // POST: AdminController/Create
        [HttpPost]
        [Route("create")]
        public async  Task<IActionResult> Create(IFormCollection form)
        {
            var model = form["model"];
            var company = form["company"];
            var price = form["price"];
            var color = form["color"];
            var description = form["description"];
            var cartype = form["cartype"];
            var productionYear = form["productionYear"];
            var mainImage = form["mainImage"];


            var props = form.Where(f => f.Key.StartsWith("prop"));
            var vals = form.Where(f => f.Key.StartsWith("val"));
            var checks = form.Where(f => f.Key.StartsWith("check"));

            var propsList = new List<PropertyFullInfo>();
            foreach(var prop in props)
            {
                var number = int.Parse(prop.Key.Substring(4));
                var propName = prop.Value;
                var value = vals.Where(v => v.Key == $"val{number}")?.FirstOrDefault().Value ?? "";
                var isMainProp = checks.Count(v => v.Key == $"check{number}") > 0;

                propsList.Add(new PropertyFullInfo()
                {
                    Property = propName,
                    Value = value,
                    IsKeyProperty = isMainProp
                });
            }

            var images = new List<ImageInfo>();
            foreach (var file in form.Files)
            {
                
                var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                var fileExtension = Path.GetExtension(file.FileName);

                
                var subPath = $"{model}-{Guid.NewGuid()}/{file.FileName}";
                var filePath = Path.Combine(_hostingEnvironment.WebRootPath, $"images/cars/{subPath}");
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                images.Add(new ImageInfo()
                {
                    Path = subPath,
                    IsMainImage = file.FileName == mainImage
                });
            }
            if (images.Where(i => i.IsMainImage).Count() == 0)
            {
                var first = images.FirstOrDefault();
                if (first != null)
                    first.IsMainImage = true;
            }

            var car = new CarFullInfo()
            {
                Price = int.Parse(price),
                CarType = cartype,
                Color = color,
                CompanyName = company,
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
                var car = await response.Content.ReadFromJsonAsync<CarFullInfo>();
                // ViewData["apiEditUrl"] = GlobalVariables.WebApiClient.BaseAddress + "Contact/edit";
                
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
			var company = form["company"];
			var price = form["price"];
			var color = form["color"];
			var description = form["description"];
			var cartype = form["cartype"];
			var productionYear = form["productionYear"];
			var mainImage = form["mainImage"];


			var props = form.Where(f => f.Key.StartsWith("prop"));
			var vals = form.Where(f => f.Key.StartsWith("val"));
			var checks = form.Where(f => f.Key.StartsWith("check"));

			var propsList = new List<PropertyFullInfo>();
			foreach (var prop in props)
			{
				var number = int.Parse(prop.Key.Substring(4));
				var propName = prop.Value;
				var value = vals.Where(v => v.Key == $"val{number}")?.FirstOrDefault().Value ?? "";
				var isMainProp = checks.Count(v => v.Key == $"check{number}") > 0;

				propsList.Add(new PropertyFullInfo()
				{
					Property = propName,
					Value = value,
					IsKeyProperty = isMainProp
				});
			}

			var images = new List<ImageInfo>();
			foreach (var file in form.Files)
			{

				var fileName = Path.GetFileNameWithoutExtension(file.FileName);
				var fileExtension = Path.GetExtension(file.FileName);


				var subPath = $"{model}-{Guid.NewGuid()}/{file.FileName}";
				var filePath = Path.Combine(_hostingEnvironment.WebRootPath, $"images/cars/{subPath}");
				Directory.CreateDirectory(Path.GetDirectoryName(filePath));
				using (var fileStream = new FileStream(filePath, FileMode.Create))
				{
					file.CopyTo(fileStream);
				}
				images.Add(new ImageInfo()
				{
					Path = subPath,
					IsMainImage = file.FileName == mainImage
				});
			}
			if (images.Where(i => i.IsMainImage).Count() == 0)
			{
				var first = images.FirstOrDefault();
				if (first != null)
					first.IsMainImage = true;
			}

			var car = new CarFullInfo()
			{
				Price = int.Parse(price),
				CarType = cartype,
				Color = color,
				CompanyName = company,
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
    }
}
