using ContentManagement.Data.Dto;
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
    public class AtaseController : BaseController
    {
        private readonly IMediator _mediator;

        public AtaseController(IMediator mediator)
        {
            _mediator = mediator;
        }

        ///<summary>Get All Atases</summary><return></return>
        [HttpGet("GetAll")]
        [Produces("application/json", "application/xml", Type = typeof(List<AtaseModelDTO>))]
        public async Task<IActionResult> GetAll()
        {
            var datas = new GetAllAtasesQuery();
            var result = await _mediator.Send(datas);
            return ReturnFormattedResponse(result);
        }

        ///<summary>Get Atase By Id</summary><return></return>
        [HttpGet("GetById")]
        [Produces("application/json", "application/xml", Type = typeof(AtaseModelDTO))]
        public async Task<IActionResult> GetById(int id)
        {
            var datas = new GetAtaseByIdQuery { Id = id };
            var result = await _mediator.Send(datas);
            return ReturnFormattedResponse(result);
        }

        ///<summary>Add Atase</summary><return></return>
        [HttpPost("Add")]
        [Produces("application/json", "application/xml", Type = typeof(AtaseModelDTO))]
        //[Authorize]
        public async Task<IActionResult> Add(AddAtaseCommand addAtaseCommand)
        {
            var result = await _mediator.Send(addAtaseCommand);
            return ReturnFormattedResponse(result);
        }

        ///<summary>Update Atase</summary><return></return>
        [HttpPut("Update")]
        [Produces("application/json", "application/xml", Type = typeof(AtaseModelDTO))]
        //[Authorize]
        public async Task<IActionResult> Update(UpdateAtaseCommand updateAtaseCommand)
        {
            var result = await _mediator.Send(updateAtaseCommand);
            return ReturnFormattedResponse(result);
        }

        ///<summary>Delete Atase</summary><return></return>
        [HttpDelete("Delete")]
        //[Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var data = new DeleteAtaseCommand { Id = id };
            var result = await _mediator.Send(data);
            return Ok(result);
        }
    }
}
