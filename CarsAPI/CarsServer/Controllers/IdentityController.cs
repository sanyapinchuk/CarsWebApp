using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarsServer.Controllers
{
	[Route("api/[controller]")]
	public class IdentityController : Controller
	{
		private readonly IConfiguration _configuration;
		public IdentityController(IConfiguration configuration)
		{
			_configuration= configuration;
		}

		[HttpOptions]
		[Authorize]
		[Route("checkAuth")]
		public IActionResult CheckAuth()
		{
			return Ok();
		}

		[HttpGet]
		[Route("GetIdentityServerAddress")]
		public IActionResult GetIdentityServerAddress()
		{
			var stri = _configuration["IdentityServerAddress"];
			return Ok(new
			{
				Address = stri
			});
		}
	}
}
