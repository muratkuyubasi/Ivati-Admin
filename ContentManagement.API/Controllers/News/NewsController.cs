using ContentManagement.Data.Dto;
using ContentManagement.DataDto;
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
    public class NewsController : BaseController
    {
        private readonly IMediator _mediator;

        public NewsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        ///<summary>Get All News</summary><return></return>
        [HttpGet("GetAll")]
        [Produces("application/json", "application/xml", Type = typeof(List<NewsDTO>))]
        public async Task<IActionResult> GetAll()
        {
            var datas = new GetAllNewsQuery();
            var result = await _mediator.Send(datas);
            return ReturnFormattedResponse(result);
        }

        ///<summary>Get News By Id</summary><return></return>
        [HttpGet("GetById")]
        [Produces("application/json", "application/xml", Type = typeof(NewsDTO))]
        public async Task<IActionResult> GetById(int id)
        {
            var datas = new GetNewsByIdQuery { Id = id };
            var result = await _mediator.Send(datas);
            return ReturnFormattedResponse(result);
        }

        ///<summary>Add News</summary><return></return>
        [HttpPost("Add")]
        [Authorize]
        [Produces("application/json", "application/xml", Type = typeof(NewsDTO))]
        public async Task<IActionResult> Add(AddNewsCommand add)
        {
            var result = await _mediator.Send(add);
            return ReturnFormattedResponse(result);
        }

        ///<summary>Update News</summary><return></return>
        [HttpPut("Update")]
        [Authorize]
        [Produces("application/json", "application/xml", Type = typeof(NewsDTO))]
        public async Task<IActionResult> Update(UpdateNewsCommand update)
        {
            var result = await _mediator.Send(update);
            return ReturnFormattedResponse(result);
        }

        ///<summary>Delete News</summary><return></return>
        [HttpDelete("Delete")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var data = new DeleteNewsCommand { Id = id };
            var result = await _mediator.Send(data);
            return Ok(result);
        }
    }
}
