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
    public class MosqueController : BaseController
    {
        private readonly IMediator _mediator;

        public MosqueController(IMediator mediator)
        {
            _mediator = mediator;
        }

        ///<summary>Get All Mosques</summary><return></return>
        [HttpGet("GetAll")]
        [Produces("application/json", "application/xml", Type = typeof(List<MosqueDTO>))]
        public async Task<IActionResult> GetAll()
        {
            var datas = new GetAllMosquesQuery();
            var result = await _mediator.Send(datas);
            return ReturnFormattedResponse(result);
        }

        ///<summary>Get Mosque By Id</summary><return></return>
        [HttpGet("GetById")]
        [Produces("application/json", "application/xml", Type = typeof(MosqueDTO))]
        public async Task<IActionResult> GetById(int id)
        {
            var datas = new GetMosqueByIdQuery { Id = id };
            var result = await _mediator.Send(datas);
            return ReturnFormattedResponse(result);
        }

        ///<summary>Add Mosque</summary><return></return>
        [HttpPost("Add")]
        [Produces("application/json", "application/xml", Type = typeof(MosqueDTO))]
        [Authorize]
        public async Task<IActionResult> Add(AddMosqueCommand add)
        {
            var result = await _mediator.Send(add);
            return ReturnFormattedResponse(result);
        }

        ///<summary>Update Mosque</summary><return></return>
        [HttpPut("Update")]
        [Produces("application/json", "application/xml", Type = typeof(MosqueDTO))]
        [Authorize]
        public async Task<IActionResult> Update(UpdateMosqueCommand update)
        {
            var result = await _mediator.Send(update);
            return ReturnFormattedResponse(result);
        }

        ///<summary>Delete Mosque</summary><return></return>
        [HttpDelete("Delete")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var data = new DeleteMosqueCommand { Id = id };
            var result = await _mediator.Send(data);
            return Ok(result);
        }
    }
}
