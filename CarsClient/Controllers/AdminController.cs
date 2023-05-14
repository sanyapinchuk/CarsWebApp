using CarsClient.Helpers;
using CarsClient.Models.Dto;
using CarsClient.Services;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
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
        public AdminController(IIdentityServer4Service identityServer4Service, GlobalVariables globalVariables)
        {
            _identityServer4Service= identityServer4Service;
            _globalVariables= globalVariables;
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
                var result = await _globalVariables.WebApiClient.DeleteAsync($"https://localhost:5001/car/delete/{id}");
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
        

        // GET: AdminController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AdminController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
