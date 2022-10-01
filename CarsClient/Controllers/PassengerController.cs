using Microsoft.AspNetCore.Mvc;

namespace CarsServer.Controllers
{
    public class PassengerController : Controller
    {                
        [Route("passenger/Han_EV_Standart")]
        public IActionResult Han_EV_Standart()
        {
            return View();
        }
    }
}
