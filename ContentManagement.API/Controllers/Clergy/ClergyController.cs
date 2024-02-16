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
    public class ClergyController : BaseController
    {
        private readonly IMediator _mediator;

        public ClergyController(IMediator mediator)
        {
            _mediator = mediator;
        }

        ///<summary>Get All Clergies</summary><return></return>
        [HttpGet("GetAll")]
        [Produces("application/json", "application/xml", Type = typeof(List<ClergyDTO>))]
        public async Task<IActionResult> GetAll()
        {
            var datas = new GetAllClergiesQuery();
            var result = await _mediator.Send(datas);
            return ReturnFormattedResponse(result);
        }

        ///<summary>Get Clergy By Id</summary><return></return>
        [HttpGet("GetById")]
        [Produces("application/json", "application/xml", Type = typeof(ClergyDTO))]
        public async Task<IActionResult> GetById(int id)
        {
            var datas = new GetClergyByIdQuery { Id = id };
            var result = await _mediator.Send(datas);
            return ReturnFormattedResponse(result);
        }

        ///<summary>Add Clergy</summary><return></return>
        [HttpPost("Add")]
        [Produces("application/json", "application/xml", Type = typeof(ClergyDTO))]
        [Authorize]
        public async Task<IActionResult> Add(AddClergyCommand addClergyCommand)
        {
            var result = await _mediator.Send(addClergyCommand);
            return ReturnFormattedResponse(result);
        }

        ///<summary>Update Clergy</summary><return></return>
        [HttpPut("Update")]
        [Produces("application/json", "application/xml", Type = typeof(ClergyDTO))]
        [Authorize]
        public async Task<IActionResult> Update(UpdateClergyCommand updateClergyCommand)
        {
            var result = await _mediator.Send(updateClergyCommand);
            return ReturnFormattedResponse(result);
        }

        ///<summary>Delete Clergy</summary><return></return>
        [HttpDelete("Delete")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var data = new DeleteClergyCommand { Id = id };
            var result = await _mediator.Send(data);
            return Ok(result);
        }
    }
}
