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
    public class DebtorTypeController : BaseController
    {
        private readonly IMediator _mediator;

        public DebtorTypeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        ///<summary>Get All Debtor Types</summary><return></return>
        [HttpGet("GetAllDebtorTypes")]
        [Produces("application/json", "application/xml", Type = typeof(List<DebtorTypeDTO>))]
        public async Task<IActionResult> GetAllDebtorTypes()
        {
            var data = new GetAllDebtorTypesQuery();
            var result = await _mediator.Send(data);
            return ReturnFormattedResponse(result);
        }

        ///<summary>Get Debtor Type By Id</summary><return></return>
        [HttpGet("GetById")]
        [Produces("application/json", "application/xml", Type = typeof(DebtorTypeDTO))]
        public async Task<IActionResult> GetById(int id)
        {
            var data = new GetDebtorTypeByIdQuery { Id = id };
            var result = await _mediator.Send(data);
            return ReturnFormattedResponse(result);
        }

        ///<summary>Add New Debtor Type</summary><return></return>
        [HttpPost("Add")]
        [Produces("application/json", "application/xml", Type = typeof(DebtorTypeDTO))]
        [Authorize]
        public async Task<IActionResult> Add(AddDebtorTypeCommand command)
        {
            var data = await _mediator.Send(command);
            return ReturnFormattedResponse(data);
        }

        ///<summary>Update Debtor Type</summary><return></return>
        [HttpPut("Update")]
        [Produces("application/json", "application/xml", Type = typeof(DebtorTypeDTO))]
        [Authorize]
        public async Task<IActionResult> Update(UpdateDebtorTypeCommand command)
        {
            var data = await _mediator.Send(command);
            return Ok(data);
        }

        ///<summary>Delete Debtor Type</summary>
        [HttpDelete("Delete")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var data = new DeleteDebtorTypeCommand { Id = id };
            var result = await _mediator.Send(data);
            return Ok(result);
        }
    }
}
