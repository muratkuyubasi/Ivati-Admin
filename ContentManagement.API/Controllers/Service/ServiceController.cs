using ContentManagement.Data.Dto;
using ContentManagement.MediatR.Commands;
using ContentManagement.MediatR.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContentManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : BaseController
    {
        private readonly IMediator _mediator;

        public ServiceController(IMediator mediator) { _mediator = mediator; }


        ///<summary>Get All Services</summary><return></return>
        [HttpGet("GetList")]
        [Produces("application/json", "application/xml", Type = typeof(List<ServicesDTO>))]
        public async Task<IActionResult> GetList()
        {
            var data = new GetAllServicesQuery();
            var result = await _mediator.Send(data);
            return ReturnFormattedResponse(result);
        }

        ///<summary>Get Service By Id</summary><return></return>
        [HttpGet("GetById/{id}")]
        [Produces("application/json", "application/xml", Type = typeof(ServicesDTO))]
        public async Task<IActionResult> GetById(int id)
        {
            var data = new GetServiceByIdQuery { Id = id };
            var result = await _mediator.Send(data);
            return ReturnFormattedResponse(result);
        }

        ///<summary>Create Service</summary><return></return>
        [HttpPost("Create")]
        [Produces("application/json", "application/xml", Type = typeof(ServicesDTO))]
        public async Task<IActionResult> Create(AddServicesCommand command)
        {
            var data = await _mediator.Send(command);
            return ReturnFormattedResponse(data);
        }

        ///<summary>Update Service</summary><return></return>
        [HttpPut("Update")]
        [Produces("application/json", "application/xml", Type = typeof(ServicesDTO))]
        public async Task<IActionResult> Update(UpdateServicesCommand command)
        {
            var data = await _mediator.Send(command);
            return ReturnFormattedResponse(data);
        }

        ///<summary>Delete Service</summary><return></return>
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Update(int id)
        {
            var data = new DeleteServicesCommand { Id = id };
            var result = await _mediator.Send(data);
            return ReturnFormattedResponse(result);
        }
    }
}
