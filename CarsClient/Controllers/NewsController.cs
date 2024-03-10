using Microsoft.AspNetCore.Mvc;

namespace CarsClient.Controllers
{
    [Route("all-news")]
    public class NewsController : Controller
    {
	    private GlobalVariables _globalVariables;
	    private readonly IConfiguration _configuration;
	    public NewsController(GlobalVariables globalVariables,
		    IConfiguration configuration)
	    {
		    _globalVariables = globalVariables;
		    _configuration = configuration;
	    }

		[Route("")]
        public async Task<IActionResult> News()
        {
	        ViewData["Postfix"] = _globalVariables.Postfix;
			return View();
        }

        [Route("{newsName}")]
        public IActionResult GetNews([FromRoute] string newsName)
        {
	        ViewData["Postfix"] = _globalVariables.Postfix;
			return View(newsName);
        }
    }
}