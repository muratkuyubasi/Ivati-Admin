using ContentManagement.Data.Dto;
using ContentManagement.Data.Models;
using ContentManagement.MediatR.Commands;
using ContentManagement.MediatR.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContentManagement.API.Controllers.Airport
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirportController : BaseController
    {
        private readonly IMediator _mediator;

        public AirportController(IMediator mediator)
        {
            _mediator = mediator;
        }

        ///<summary>Get All Airports</summary><return></return>
        [HttpGet("GetAllAirports")]
        [Produces("application/json", "application/xml", Type = typeof(List<AirportDTO>))]
        public async Task<IActionResult> GetAllAirports()
        {
            var data = new GetAllAirportsQuery();
            var result = await _mediator.Send(data);
            return ReturnFormattedResponse(result);
        }

        ///<summary>Get Airport By Id</summary><return></return>
        [HttpGet("GetById")]
        [Produces("application/json", "application/xml", Type = typeof(AirportDTO))]
        public async Task<IActionResult> GetById(int id)
        {
            var data = new GetAirportByIdQuery { Id = id};
            var result = await _mediator.Send(data);
            return ReturnFormattedResponse(result);
        }

        ///<summary>Add New Airport</summary><return></return>
        [HttpPost("Add")]
        [Produces("application/json", "application/xml", Type = typeof(AirportDTO))]
        [Authorize]
        public async Task<IActionResult> Add(AddAirportCommand addAirportCommand)
        {
            var data = await _mediator.Send(addAirportCommand);
            return ReturnFormattedResponse(data);
        }

        ///<summary>Update Airport</summary><return></return>
        [HttpPut("Update")]
        [Produces("application/json", "application/xml", Type = typeof(AirportDTO))]
        [Authorize]
        public async Task<IActionResult> Update(UpdateAirportCommand updateAirportCommand)
        {
            var data = await _mediator.Send(updateAirportCommand);
            return Ok(data);
        }

        ///<summary>Delete Airport</summary>
        [HttpDelete("Delete")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var data = new DeleteAirportCommand { Id = id };
            var result = await _mediator.Send(data);
            return Ok(result);
        }
    }
}
