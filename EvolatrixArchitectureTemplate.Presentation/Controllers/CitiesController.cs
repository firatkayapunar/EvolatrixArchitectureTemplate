using EvolatrixArchitectureTemplate.CommandQueryModel.CQRS.Commands.Requests.CityCommandRequests;
using EvolatrixArchitectureTemplate.CommandQueryModel.CQRS.Queries.Requests.CityQueryRequests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DriveNow.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private IMediator _mediator;
        public CitiesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCities([FromQuery] GetAllCityQueryRequest requestModel)
        {
            var values = await _mediator.Send(requestModel);
            return Ok(values);
        }

        [HttpGet("GetCityById/{id}")]
        public async Task<IActionResult> GetCityById([FromRoute] int id)
        {
            var requestModel = new GetCityByIdQueryRequest();
            requestModel.Id = id;
            var value = await _mediator.Send(requestModel);
            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCityCommandRequest requestModel)
        {
            var response = await _mediator.Send(requestModel);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateCityCommandRequest requestModel)
        {
            var response = await _mediator.Send(requestModel);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var requestModel = new DeleteCityCommandRequest();
            requestModel.Id = id;
            var response = await _mediator.Send(requestModel);
            return Ok(response);
        }
    }
}
