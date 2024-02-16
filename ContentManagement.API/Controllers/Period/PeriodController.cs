using ContentManagement.Data.Dto;
using ContentManagement.MediatR.Commands;
using ContentManagement.MediatR.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContentManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeriodController : BaseController
    {
        private readonly IMediator _mediator;

        public PeriodController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Add Hac Period
        /// </summary>
        /// <returns></returns>
        [HttpPost("AddHP")]
        [Authorize]
        [Produces("application/json", "application/xml", Type = typeof(HacPeriodDTO))]
        public async Task<IActionResult> Add(AddHacPeriodCommand addHacPeriodCommand)
        {
            var data = await _mediator.Send(addHacPeriodCommand);
            return ReturnFormattedResponse(data);
        }
        /// <summary>
        /// Add Umre Period
        /// </summary>
        /// <returns></returns>
        [HttpPost("AddUP")]
        [Authorize]
        [Produces("application/json", "application/xml", Type = typeof(UmrePeriodDTO))]
        public async Task<IActionResult> Add(AddUmrePeriodCommand command)
        {
            var data = await _mediator.Send(command);
            return ReturnFormattedResponse(data);
        }

        /// <summary>
        /// Update Hac Period
        /// </summary>
        /// <returns></returns>
        [HttpPut("UpdateHP")]
        [Authorize]
        [Produces("application/json", "application/xml", Type = typeof(HacPeriodDTO))]
        public async Task<IActionResult> Update(UpdateHacPeriodCommand command)
        {
            var data = await _mediator.Send(command);
            return ReturnFormattedResponse(data);
        }
        /// <summary>
        /// Update Umre Period
        /// </summary>
        /// <returns></returns>
        [HttpPut("UpdateUP")]
        [Authorize]
        [Produces("application/json", "application/xml", Type = typeof(UmrePeriodDTO))]
        public async Task<IActionResult> Update(UpdateUmrePeriodCommand command)
        {
            var data = await _mediator.Send(command);
            return ReturnFormattedResponse(data);
        }

        /// <summary>
        /// Get List Hac Periods
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetListHP")]
        [Produces("application/json", "application/xml", Type = typeof(List<HacPeriodDTO>))]
        public async Task<IActionResult> GetListHP()
        {
            var data = new GetHacPeriodListQuery { };
            var result = await _mediator.Send(data);
            return ReturnFormattedResponse(result);
        }


        /// <summary>
        /// Get List Umre Periods
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetListUP")]
        [Produces("application/json", "application/xml", Type = typeof(List<UmrePeriodDTO>))]
        public async Task<IActionResult> GetListUP()
        {
            var data = new GetUmrePeriodListQuery { };
            var result = await _mediator.Send(data);
            return ReturnFormattedResponse(result);
        }
    }
}
