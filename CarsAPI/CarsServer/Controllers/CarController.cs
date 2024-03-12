using System.Runtime.CompilerServices;
using Applicaton.Cars.Commands.CreateCar;
using Applicaton.Cars.Commands.DeleteCar;
using Applicaton.Cars.Commands.UpdateCar;
using Applicaton.Cars.Queries.GetCarFilterConfig;
using Applicaton.Cars.Queries.GetCarFullInfo;
using Applicaton.Cars.Queries.GetCarsList;
using CarsServer.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace CarsServer.Controllers
{
    [Route("api/[controller]/[action]")]
    public class CarController: BaseController
    {
        private readonly AuthHelperService _authHelperService;

        public CarController(AuthHelperService authHelperService)
        {
            _authHelperService = authHelperService;
        }

        [HttpGet]
        public async Task<ActionResult<CarListDto>> GetAll(
	        [FromQuery] Guid[]? carTypeIds,
	        [FromQuery] Guid[]? powerReserveParamIds, 
	        [FromQuery] int? priceFrom, 
	        [FromQuery] int? priceTo, 
	        [FromQuery] Guid[]? batteryCapacity,
	        [FromQuery] Guid[]? driveModeIds,
            [FromQuery] Guid[]? manufactures)
        {
            var query = new GetCarListQuery()
            {
                UserId = Guid.NewGuid(),
                CarTypeIds = carTypeIds,
                PowerReserveParamIds = powerReserveParamIds,
                PriceFrom = priceFrom,
                PriceTo = priceTo,
                ManufacturesIds = manufactures,
                BatteryCapacityIds = batteryCapacity,
                DriveModeIds = driveModeIds
            };
            var dto = await Mediator.Send(query);

            return Ok(dto);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<CarFullInfoDto>> Get(Guid id)
        {
            var query = new GetCarFullInfoQuery()
            {
                Id = id
            };
            var dto = await Mediator.Send(query);
            return Ok(dto);
        }

        [HttpPost]
		//[AuthAttribute]
		public async Task<ActionResult<Guid>> Create([FromBody] CreateCarCommand carDto)
        {
            _authHelperService.EnsureAccessTokenExists(Request);

            var carId = await Mediator.Send(carDto);
                return Ok(carId);
        }

        [HttpPut]
		//[AuthAttribute]
		public async Task<IActionResult> Update([FromBody] UpdateCarCommand carDto)
        {
            _authHelperService.EnsureAccessTokenExists(Request);

            await Mediator.Send(carDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
		//[AuthAttribute]
		public async Task<IActionResult> Delete(Guid id)
        {
            _authHelperService.EnsureAccessTokenExists(Request);

            var command = new DeleteCarCommand()
            {
                Id = id
            };
            await Mediator.Send(command);
            return NoContent();
        }

        [ResponseCache(Duration = 86400)]
        [HttpGet]
        public async Task<ActionResult<CarFilterConfigDto>> FilterConfig()
        {
            var query = new GetCarFilterConfigQueryHandler.Request();
            var dto = await Mediator.Send(query);

            return Ok(dto);
        }
    }
}
