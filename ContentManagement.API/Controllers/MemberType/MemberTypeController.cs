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

namespace ContentManagement.API.Controllers.MemberType
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberTypeController : BaseController
    {
        private readonly IMediator _mediator;

        public MemberTypeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        ///<summary>Get All Member Types</summary><return></return>
        [HttpGet("GetAll")]
        [Produces("application/json", "application/xml", Type = typeof(List<MemberTypeDTO>))]
        public async Task<IActionResult> GetAll()
        {
            var data = new GetAllMemberTypesQuery();
            var result = await _mediator.Send(data);
            return ReturnFormattedResponse(result);
        }

        ///<summary>Get Member Type By Id</summary><return></return>
        [HttpGet("GetById")]
        [Produces("application/json", "application/xml", Type = typeof(MemberTypeDTO))]
        public async Task<IActionResult> GetById(int id)
        {
            var data = new GetMemberTypeByIdQuery { Id = id };
            var result = await _mediator.Send(data);
            return ReturnFormattedResponse(result);
        }

        ///<summary>Add New Member Type</summary><return></return>
        [HttpPost("Add")]
        [Produces("application/json", "application/xml", Type = typeof(MemberTypeDTO))]
        [Authorize]
        public async Task<IActionResult> Add(AddMemberTypeCommand command)
        {
            var data = await _mediator.Send(command);
            return ReturnFormattedResponse(data);
        }

        ///<summary>Update Member Type</summary><return></return>
        [HttpPut("Update")]
        [Produces("application/json", "application/xml", Type = typeof(MemberTypeDTO))]
        [Authorize]
        public async Task<IActionResult> Update(UpdateMemberTypeCommand command)
        {
            var data = await _mediator.Send(command);
            return Ok(data);
        }

        ///<summary>Delete Member Type</summary>
        [HttpDelete("Delete")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var data = new DeleteMemberTypeCommand { Id = id };
            var result = await _mediator.Send(data);
            return Ok(result);
        }
    }
}
