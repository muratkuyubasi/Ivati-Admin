using ContentManagement.Data.Dto;
using ContentManagement.MediatR.Commands;
using ContentManagement.MediatR.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContentManagement.API.Controllers.City
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : BaseController
    {
        private readonly IMediator _mediator;

        public CityController(IMediator mediator)
        {
            _mediator = mediator;
        }

        ///<summary>Get All Cities</summary><return></return>
        [HttpGet("GetList")]
        [Produces("application/json", "application/xml", Type = typeof(List<CityDTO>))]
        public async Task<IActionResult> GetAllAirports()
        {
            var data = new GetAllCitiesQuery();
            var result = await _mediator.Send(data);
            return ReturnFormattedResponse(result);
        }

        ///<summary>Get All Default Cities</summary><return></return>
        [HttpGet("GetAllDefaultCities")]
        [Produces("application/json", "application/xml", Type = typeof(List<CitySimpleDTO>))]
        public async Task<IActionResult> GetAllDefaultCities()
        {
            var data = new GetDefaultAreaQuery();
            var result = await _mediator.Send(data);
            return ReturnFormattedResponse(result);
        }

        ///<summary>Get City By Id</summary><return></return>
        [HttpGet("GetById")]
        [Produces("application/json", "application/xml", Type = typeof(CityDTO))]
        public async Task<IActionResult> GetById(int id)
        {
            var data = new GetCityByIdQuery { Id = id };
            var result = await _mediator.Send(data);
            return ReturnFormattedResponse(result);
        }

        ///<summary>Add New City</summary><return></return>
        [HttpPost("Add")]
        [Authorize]
        [Produces("application/json", "application/xml", Type = typeof(CityDTO))]
        public async Task<IActionResult> Add(AddCityCommand command)
        {
            var data = await _mediator.Send(command);
            return ReturnFormattedResponse(data);
        }

        ///<summary>Update City</summary><return></return>
        [HttpPut("Update")]
        [Authorize]
        [Produces("application/json", "application/xml", Type = typeof(CityDTO))]
        public async Task<IActionResult> Update(UpdateCityCommand command)
        {
            var data = await _mediator.Send(command);
            return Ok(data);
        }

        ///<summary>Delete Airport</summary>
        [HttpDelete("Delete")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var data = new DeleteCityCommand { Id = id };
            var result = await _mediator.Send(data);
            return Ok(result);
        }
    }
}
