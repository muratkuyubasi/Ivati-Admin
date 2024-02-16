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
    public class RoomTypeController : BaseController
    {
        private readonly IMediator _mediator;
        public RoomTypeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        ///<summary>Get All Room Types</summary><return></return>
        [HttpGet("GetAllRoomTypes")]
        [Produces("application/json", "application/xml", Type = typeof(List<RoomTypeDTO>))]
        public async Task<IActionResult> GetAllRoomTypes()
        {
            var data = new GetAllRoomTypesQuery();
            var result = await _mediator.Send(data);
            return ReturnFormattedResponse(result); 
        }

        ///<summary>Get Room Type By Id</summary><return></return>
        [HttpGet("GetById")]
        [Produces("application/json", "application/xml", Type = typeof(RoomTypeDTO))]
        public async Task<IActionResult> GetById(int id)
        {
            var data = new GetRoomTypeByIdQuery { Id = id};
            var result = await _mediator.Send(data);
            return ReturnFormattedResponse(result);
        }

        ///<summary>Add New Room Type</summary><return></return>
        [HttpPost("Add")]
        [Produces("application/json", "application/xml", Type = typeof(RoomTypeDTO))]
        [Authorize]
        public async Task<IActionResult> Add(AddRoomTypeCommand addRoomTypeCommand)
        {
            var result = await _mediator.Send(addRoomTypeCommand);
            return ReturnFormattedResponse(result);
        }

        ///<summary>Update Room Type</summary><return></return>
        [HttpPut("Update")]
        [Produces("application/json", "application/xml", Type = typeof(RoomTypeDTO))]
        [Authorize]
        public async Task<IActionResult> Update(UpdateRoomTypeCommand updateRoomType)
        {
            var data = await _mediator.Send(updateRoomType);
            return Ok(data);
        }

        ///<summary>Delete Room Type</summary>
        [HttpDelete("Delete")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var data = new DeleteRoomTypeCommand { Id = id };
            var result = await _mediator.Send(data);
            return Ok(data);
        }
    }
}
