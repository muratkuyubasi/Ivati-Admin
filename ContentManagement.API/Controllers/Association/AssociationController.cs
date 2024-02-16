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
    public class AssociationController : BaseController
    {

        private readonly IMediator _mediator;

        public AssociationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        ///<summary>Get All Associations</summary>
        ///<return></return>
        [HttpGet("GetAllAssociations")]
        [Produces("application/json", "application/xml", Type = typeof(List<AssociationDTO>))]
        public async Task<IActionResult> GetAllAssociations()
        {
            var data = new GetAllAssociationQuery { };
            var result = await _mediator.Send(data);
            return ReturnFormattedResponse(result);
        }

        ///<summary>Get Association By Id</summary><return></return>
        [HttpGet("GetById")]
        [Produces("application/json", "application/xml", Type = typeof(AssociationDTO))]
        public async Task<IActionResult> GetById(int id)
        {
            var data = new GetAssociationByIdQuery { Id = id };
            var result = await _mediator.Send(data);
            return ReturnFormattedResponse(result);
        }

        ///<summary>Add New Association</summary><return></return>
        [HttpPost("Add")]
        [Produces("application/json", "application/xml", Type = typeof(AssociationDTO))]
        [Authorize]
        public async Task<IActionResult> Add(AddAssociationCommand addAssociationCommand)
        {
            var data = await _mediator.Send(addAssociationCommand);
            return ReturnFormattedResponse(data);
        }

        ///<summary>Update Association</summary><return></return>
        [HttpPut("Update")]
        [Produces("application/json", "application/xml", Type = typeof(AssociationDTO))]
        [Authorize]
        public async Task<IActionResult> Update(UpdateAssociationCommand updateAssociationCommand)
        {
            var data = await _mediator.Send(updateAssociationCommand);
            return Ok(data);
        }

        ///<summary>Delete Association</summary>
        [HttpDelete("Delete")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var data = new DeleteAssociationCommand { Id = id };
            var result =  await _mediator.Send(data);
            return Ok(result);
        }
    }
}
