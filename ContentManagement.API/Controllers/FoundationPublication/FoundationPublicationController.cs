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
    public class FoundationPublicationController : BaseController
    {
        private readonly IMediator _mediator;

        public FoundationPublicationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        ///<summary>Get All Foundation Publications</summary><return></return>
        [HttpGet("GetAll")]
        [Produces("application/json", "application/xml", Type = typeof(List<FoundationPublicationDTO>))]
        public async Task<IActionResult> GetAll()
        {
            var datas = new GetAllFoundationPublicationQuery();
            var result = await _mediator.Send(datas);
            return ReturnFormattedResponse(result);
        }

        ///<summary>Get Foundation Publication By Id</summary><return></return>
        [HttpGet("GetById")]
        [Produces("application/json", "application/xml", Type = typeof(FoundationPublicationDTO))]
        public async Task<IActionResult> GetById(int id)
        {
            var datas = new GetFoundationPublicationByIdQuery { Id = id };
            var result = await _mediator.Send(datas);
            return ReturnFormattedResponse(result);
        }

        ///<summary>Add Foundation Publication</summary><return></return>
        [HttpPost("Add")]
        [Produces("application/json", "application/xml", Type = typeof(FoundationPublicationDTO))]
        [Authorize]
        public async Task<IActionResult> Add(AddFoundationPublicationCommand addFoundationPublicationCommand)
        {
            var result = await _mediator.Send(addFoundationPublicationCommand);
            return ReturnFormattedResponse(result);
        }

        ///<summary>Update Foundation Publication</summary><return></return>
        [HttpPut("Update")]
        [Produces("application/json", "application/xml", Type = typeof(FoundationPublicationDTO))]
        [Authorize]
        public async Task<IActionResult> Update(UpdateFoundationPublicationCommand update)
        {
            var result = await _mediator.Send(update);
            return ReturnFormattedResponse(result);
        }

        ///<summary>Delete Foundation Publication</summary><return></return>
        [HttpDelete("Delete")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var data = new DeleteFoundationPublicationCommand { Id = id };
            var result = await _mediator.Send(data);
            return Ok(result);
        }
    }
}
