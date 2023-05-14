using CarsClient.Helpers;
using CarsClient.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace CarsClient.Controllers
{
    [Route("admin")]
    public class AdminController : Controller
    {
        // GET: AdminController
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Options, "identity/checkAuth");
          //  httpRequest.Headers.Add("Access-Control-Request-Method", "GET");
           // httpRequest.Headers.Add("Access-Control-Request-Headers", "X-Requested-With, Content-Type, Authorization");

            var response = await GlobalVariables.WebApiClient.SendAsync(httpRequest);

            if (response.IsSuccessStatusCode)
            {
                var response2 = await GlobalVariables.WebApiClient.GetAsync("car/getall");
                if (response2.IsSuccessStatusCode)
                {
                    var cars = await response2.Content.ReadFromJsonAsync<CarList>();
                    // ViewData["apiEditUrl"] = GlobalVariables.WebApiClient.BaseAddress + "Contact/edit";
                    return View(cars.Cars);
                }
                else
                    return StatusCode((int)response2.StatusCode);
            }
            else
            {
                if(response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    if (string.IsNullOrWhiteSpace(GlobalVariables.IdentityServerUrl))
                    {
						var response2 = await GlobalVariables.WebApiClient.GetAsync("identity/getIdentityServerAddress");
						if (response2.IsSuccessStatusCode)
						{
                            var objStr = await response2.Content.ReadAsStringAsync();
                            var obj = JsonConvert.DeserializeAnonymousType(objStr, new { Address = ""});
                            GlobalVariables.IdentityServerUrl = obj.Address;
						}
						else
							return StatusCode((int)response2.StatusCode);
					}
					string currUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.Path}{HttpContext.Request.QueryString}";

					return Redirect(GlobalVariables.IdentityServerUrl + $"auth/login?returnUrl={currUrl}");
				}
				return StatusCode((int)response.StatusCode);
			}
        }


        [HttpDelete("{id}")]
        [Route("delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await GlobalVariables.WebApiClient.DeleteAsync($"car/delete/{id}");
			if (response.IsSuccessStatusCode)
            {
                return Ok();
            }
            else
            {
                if(response.StatusCode == HttpStatusCode.Unauthorized)
                {
					if (string.IsNullOrWhiteSpace(GlobalVariables.IdentityServerUrl))
					{
						var response2 = await GlobalVariables.WebApiClient.GetAsync("identity/getIdentityServerAddress");
                        if (response2.IsSuccessStatusCode)
                        {
							var objStr = await response2.Content.ReadAsStringAsync();
							var obj = JsonConvert.DeserializeAnonymousType(objStr, new { Address = "" });
							GlobalVariables.IdentityServerUrl = obj.Address;
						}
                        else
                            return StatusCode((int)response2.StatusCode);
					}
					return Redirect(GlobalVariables.IdentityServerUrl + "auth/login");
				}
				return StatusCode((int)response.StatusCode);
			}
		}
        // GET: AdminController/Details/5
        public ActionResult Details(int id)
        {
            return View();
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

        // GET: AdminController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AdminController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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
