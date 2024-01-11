using Applicaton.Cars.Queries.GetCarFullInfo;
using Applicaton.News.Commands;
using Applicaton.News.Requests;
using CarsServer.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace CarsServer.Controllers
{
    [Route("api/[controller]/[action]")]
    public class NewsController: BaseController
    {
        [HttpGet]
        public async Task<ActionResult<NewsDto>> GetAll()
        {
            var request = new GetAllNewsRequestHandler.Request();

            var dto = await Mediator.Send(request);

            return Ok(dto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CarFullInfoDto>> Get(Guid id)
        {
            var query = new GetNewsRequestHandler.Request(id);
            var dto = await Mediator.Send(query);
            return Ok(dto);
        }

        [HttpPost]
		[AuthAttribute]
		public async Task<ActionResult<Guid>> Create([FromBody] CreateNewsCommand newsDto)
        {
            var newsId = await Mediator.Send(newsDto);
                return Ok(newsId);
        }

        [HttpPut]
		[AuthAttribute]
		public async Task<IActionResult> Update([FromBody] UpdateNewsCommand newsDto)
        {
            await Mediator.Send(newsDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
		[AuthAttribute]
		public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteNewsCommand
            {
                NewsId = id
            };
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
