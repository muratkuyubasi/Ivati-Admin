using ContentManagement.Data.Dto;
using ContentManagement.MediatR.Commands;
using ContentManagement.MediatR.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContentManagement.API.Controllers.Age
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgeController : BaseController
    {
        private readonly IMediator _mediator;

        public AgeController(IMediator mediator)
        {
            _mediator = mediator;
        }


        ///<summary> Get All Age Ranges </summary><returns></returns>
        [HttpGet("GetAll")]
        [Produces("application/json", "application/xml", Type = typeof(List<AgeDTO>))]
        public async Task<IActionResult> GetAll()
        {
            var data = new GetAllAgeQuery();
            var result = await _mediator.Send(data);
            return ReturnFormattedResponse(result);
        }

        ///<summary> Get Age By Date </summary><returns></returns>
        [HttpGet("GetAgeByDate")]
        [Produces("application/json", "application/xml", Type = typeof(AgeDTO))]
        public async Task<IActionResult> GetAgeByDate(DateTime date)
        {
            var data = new GetAgeByDateQuery { Date = date };
            var result = await _mediator.Send(data);
            return ReturnFormattedResponse(result);
        }

        ///<summary> Get Age Range By Id</summary><returns></returns>
        [HttpGet("GetById")]
        [Produces("application/json", "application/xml", Type = typeof(AgeDTO))]
        public async Task<IActionResult> GetById(int id)
        {
            var data = new GetAgeByIdQuery { Id = id };
            var result = await _mediator.Send(data);
            return ReturnFormattedResponse(result);
        }

        ///<summary> Get Amount By Min Age between Max Age</summary><returns></returns>
        [HttpGet("GetByMinAndMaxAge")]
        [Produces("application/json", "application/xml", Type = typeof(AgeDTO))]
        public async Task<IActionResult> GetByMinAndMaxAge(int minAge, int maxAge)
        {
            var data = new GetAgeByMaxAndMinValueQuery { MinAge = minAge, MaxAge = maxAge };
            var result = await _mediator.Send(data);
            return ReturnFormattedResponse(result);
        }

        ///<summary> Add New Age Range </summary><returns></returns>
        [HttpPost("Add")]
        [Authorize]
        [Produces("application/json", "application/xml", Type = typeof(AgeDTO))]
        public async Task<IActionResult> Add(AddAgeCommand command)
        {
            var data = await _mediator.Send(command);
            return Ok(data);
        }

        ///<summary> Change Annual Dues</summary><returns></returns>
        [HttpPost("Change")]
        [Authorize]
        [Produces("application/json", "application/xml", Type = typeof(AgeDTO))]
        public async Task<IActionResult> Change(decimal dues)
        {
            var data = new ChangeAnnualDuesCommand { Dues = dues };
            var result = await _mediator.Send(data);
            return Ok(result);
        }


        ///<summary> Update Age Range </summary><returns></returns>
        [HttpPut("Update")]
        [Authorize]
        [Produces("application/json", "application/xml", Type = typeof(AgeDTO))]
        public async Task<IActionResult> Update(UpdateAgeCommand command)
        {
            var data = await _mediator.Send(command);
            return Ok(data);
        }

        ///<summary> Delete Age Range </summary><returns></returns>
        [HttpDelete("Delete")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var data = new DeleteAgeCommand { Id = id };
            var result = await _mediator.Send(data);
            return Ok(result);
        }

    }
}
