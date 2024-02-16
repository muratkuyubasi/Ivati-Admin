using ContentManagement.Data.Dto;
using ContentManagement.MediatR.Commands;
using ContentManagement.MediatR.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContentManagement.API.Controllers.Country
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : BaseController
    {
        private readonly IMediator _mediator;

        public CountryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        ///<summary>Get All Countries</summary><return></return>
        [HttpGet("GetList")]
        [Produces("application/json", "application/xml", Type = typeof(List<CountryDTO>))]
        public async Task<IActionResult> GetAllAirports()
        {
            var data = new GetAllCountriesQuery();
            var result = await _mediator.Send(data);
            return ReturnFormattedResponse(result);
        }

        ///<summary>Get Country By Id</summary><return></return>
        [HttpGet("GetById")]
        [Produces("application/json", "application/xml", Type = typeof(CountryDTO))]
        public async Task<IActionResult> GetById(int id)
        {
            var data = new GetCountryByIdQuery { Id = id };
            var result = await _mediator.Send(data);
            return ReturnFormattedResponse(result);
        }

        ///<summary>Get Country By Langcode</summary><return></return>
        [HttpGet("GetByLangcode")]
        [Produces("application/json", "application/xml", Type = typeof(CountryDTO))]
        public async Task<IActionResult> GetByLangcode(string langcode)
        {
            var data = new GetCountryByLangcodeQuery { Langcode = langcode };
            var result = await _mediator.Send(data);
            return ReturnFormattedResponse(result);
        }

        ///<summary>Add New Country</summary><return></return>
        [HttpPost("Add")]
        [Authorize]
        [Produces("application/json", "application/xml", Type = typeof(CountryDTO))]
        public async Task<IActionResult> Add(AddCountryCommand command)
        {
            var data = await _mediator.Send(command);
            return ReturnFormattedResponse(data);
        }

        ///<summary>Update Country</summary><return></return>
        [HttpPut("Update")]
        [Authorize]
        [Produces("application/json", "application/xml", Type = typeof(CountryDTO))]
        public async Task<IActionResult> Update(UpdateCountryCommand command)
        {
            var data = await _mediator.Send(command);
            return Ok(data);
        }

        ///<summary>Delete Airport</summary>
        [HttpDelete("Delete")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var data = new DeleteCountryCommand { Id = id };
            var result = await _mediator.Send(data);
            return Ok(result);
        }
    }
}
