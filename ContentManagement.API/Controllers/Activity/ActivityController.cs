using ContentManagement.Data.Dto;
using ContentManagement.MediatR.Commands;
using ContentManagement.MediatR.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContentManagement.API.Controllers.Activity
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityController : BaseController
    {
        private readonly IMediator _mediator;

        public ActivityController(IMediator mediator)
        {
            _mediator = mediator;
        }

        ///<summary>Get All Activities</summary><return></return>
        [HttpGet("GetAllActivities")]
        [Produces("application/json", "application/xml", Type = typeof(List<ActivityDTO>))]
        public async Task<IActionResult> GetAllActivities()
        {
            var data = new GetAllActivitiesQuery();
            var result = await _mediator.Send(data);
            return ReturnFormattedResponse(result);
        }

        ///<summary>Get Activity By Id</summary><return></return>
        [HttpGet("GetById")]
        [Produces("application/json", "application/xml", Type = typeof(ActivityDTO))]
        public async Task<IActionResult> GetById(int id)
        {
            var data = new GetActivityByIdQuery { Id = id };
            var result = await _mediator.Send(data);
            return ReturnFormattedResponse(result);
        }

        ///<summary>Create Activity</summary><return></return>
        [HttpPost("Create")]
        [Authorize]
        [Produces("application/json" , "application/xml", Type =typeof(ActivityDTO))]
        public async Task<IActionResult> Create(AddActivityCommand commnand)
        {
            var result = await _mediator.Send(commnand);
            return Ok(result);
        }

        ///<summary>Update Activity</summary><return></return>
        [HttpPut("Update")]
        [Authorize]
        [Produces("application/json", "application/xml", Type = typeof(ActivityDTO))]
        public async Task<IActionResult> Update(UpdateActivityCommand commnand)
        {
            var result = await _mediator.Send(commnand);
            return Ok(result);
        }

        ///<summary>Delete Activity</summary><return></return>
        [HttpDelete("Delete")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var data = new DeleteActivityCommand { Id = id };
            var result = await _mediator.Send(data);
            return Ok(result);
        }
    }
}
