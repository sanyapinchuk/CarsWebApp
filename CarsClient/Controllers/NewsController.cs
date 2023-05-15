using Microsoft.AspNetCore.Mvc;

namespace CarsClient.Controllers
{
	[Route("all_news")]
	public class All_NewsController : Controller
	{
		[HttpGet]
		[Route("battery_life")]
		public IActionResult Battery_life()
		{
			return View();
		}
		[HttpGet]
		[Route("how_to_choose_a_car")]
		public IActionResult how_to_choose_a_car()
		{
			return View();
		}
		[HttpGet]
		[Route("TESLA_vs_BYD")]
		public IActionResult TESLA_vs_BYD()
		{
			return View();
		}
	}
}
