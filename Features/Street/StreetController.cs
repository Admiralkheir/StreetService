using MediatR;
using Microsoft.AspNetCore.Mvc;
using StreetService.Features.Street.AddPointToStreet;
using StreetService.Features.Street.CreateStreet;
using StreetService.Features.Street.DeleteStreet;
using StreetService.Features.Street.GetStreet;

namespace StreetService.Features.Street
{
    [ApiController]
    [Route("[controller]")]
    public class StreetController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StreetController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("AddPointToStreet")]
        public async Task<IActionResult> AddPointToStreet([FromBody] AddPointToStreetDto addPointToStreetDto,CancellationToken cancellationToken)
        {
            var request = new AddPointToStreetRequest(addPointToStreetDto.StreetId, addPointToStreetDto.point);

            var response = await _mediator.Send(request, cancellationToken);

            return Ok(response);
        }

        [HttpPost("CreateStreet")]
        public async Task<IActionResult> CreateStreet([FromBody] CreateStreetRequestDto createStreetRequestDto, CancellationToken cancellationToken)
        {
            var request = new CreateStreetRequest(createStreetRequestDto.StreetName, createStreetRequestDto.Capacity, createStreetRequestDto.Geometry);

            var response = await _mediator.Send(request,cancellationToken);

            return Ok(response);
        }

        [HttpGet("GetStreet")]
        public async Task<IActionResult> GetStreet([FromQuery] int streetId, CancellationToken cancellationToken)
        {
            var request = new GetStreetRequest(streetId);

            var response = await _mediator.Send(request, cancellationToken);

            return Ok(response);
        }

        [HttpDelete("DeleteStreet")]
        public async Task<IActionResult> DeleteStreet([FromQuery] int streetId, CancellationToken cancellationToken)
        {
            var request = new DeleteStreetRequest(streetId);

            var response = await _mediator.Send(request, cancellationToken);

            return Ok(response);
        }
    }
}
