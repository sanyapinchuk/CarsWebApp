using Applicaton.Cars.Commands.CreateCar;
using Applicaton.Cars.Commands.DeleteCar;
using Applicaton.Cars.Commands.UpdateCar;
using Applicaton.Cars.Queries.GetCarFullInfo;
using Applicaton.Cars.Queries.GetCarsList;
using Applicaton.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsServer.Controllers
{
    [Route("api/[controller]/[action]")]
    public class CarController: BaseController
    {
        [HttpGet]
        public async Task<ActionResult<CarListDto>> GetAll()
        {
            var query = new GetCarListQuery()
            {
                UserId = Guid.NewGuid()
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
        public async Task<ActionResult<Guid>> Create([FromBody] CreateCarCommand carDto)
        {
            var carId = await Mediator.Send(carDto);
            return Ok(carId);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateCarCommand carDto)
        {
            await Mediator.Send(carDto);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteCarCommand()
            {
                Id = id
            };
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
