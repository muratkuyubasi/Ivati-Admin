using ContentManagement.Data;
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
    public class ChairmanController : BaseController
    {
        private readonly IMediator _mediator;

        public ChairmanController(IMediator mediator)
        {
            _mediator = mediator;
        }

        ///<summary>Get All Chairmen</summary><return></return>
        [HttpGet("GetAll")]
        [Produces("application/json", "application/xml", Type = typeof(List<ChairmanDTO>))]
        public async Task<IActionResult> GetAll()
        {
            var datas = new GetAllChairmenQuery();
            var result = await _mediator.Send(datas);
            return ReturnFormattedResponse(result);
        }

        ///<summary>Get Chairman By Id</summary><return></return>
        [HttpGet("GetById")]
        [Produces("application/json", "application/xml", Type = typeof(ChairmanDTO))]
        public async Task<IActionResult> GetById(int id)
        {
            var datas = new GetChairmanByIdQuery { Id = id };
            var result = await _mediator.Send(datas);
            return ReturnFormattedResponse(result);
        }

        ///<summary>Add Chairman</summary><return></return>
        [HttpPost("Add")]
        [Produces("application/json", "application/xml", Type = typeof(ChairmanDTO))]
        //[Authorize]
        public async Task<IActionResult> Add(AddChairmanCommand addChairmanCommand)
        {
            var result = await _mediator.Send(addChairmanCommand);
            return ReturnFormattedResponse(result);
        }

        ///<summary>Update Chairman</summary><return></return>
        [HttpPut("Update")]
        [Produces("application/json", "application/xml", Type = typeof(ChairmanDTO))]
        //[Authorize]
        public async Task<IActionResult> Update(UpdateChairmanCommand updateChairmanCommand)
        {
            var result = await _mediator.Send(updateChairmanCommand);
            return ReturnFormattedResponse(result);
        }

        ///<summary>Delete Chairman</summary><return></return>
        [HttpDelete("Delete")]
        //[Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var data = new DeleteChairmanCommand { Id = id };
            var result = await _mediator.Send(data);
            return Ok(result);
        }
    }
}
