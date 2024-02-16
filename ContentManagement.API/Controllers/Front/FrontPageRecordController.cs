using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ContentManagement.Data.Dto;
using PT.MediatR.Commands;
using ContentManagement.MediatR.Queries;

namespace ContentManagement.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FrontPageRecordController : BaseController
    {
        IMediator _mediator;

        public FrontPageRecordController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get By Id
        /// </summary>
        /// <param name="Code"></param>
        /// <returns></returns>
        [HttpGet("{Code}")]
        [Produces("application/json", "application/xml", Type = typeof(FrontPageRecordDto))]
        public async Task<IActionResult> Get(Guid Code)
        {
            var result = await _mediator.Send(new GetFrontPageRecordQuery { Code = Code });

            return ReturnFormattedResponse(result);
        }
        /// <summary>
        /// Get All List
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Produces("application/json", "application/xml", Type = typeof(List<FrontPageRecordDto>))]
        public async Task<IActionResult> GetList()
        {
            var result = await _mediator.Send(new GetAllFrontPageRecordQuery { });

            return ReturnFormattedResponse(result);
        }

        /// <summary>
        /// Create Add FrontPageRecord.
        /// </summary>
        /// <param name="addFrontPageRecordCommand"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [Produces("application/json", "application/xml", Type = typeof(AddFrontPageRecordCommand))]
        public async Task<IActionResult> Add(AddFrontPageRecordCommand addFrontPageRecordCommand)
        {
            var result = await _mediator.Send(addFrontPageRecordCommand);
            return ReturnFormattedResponse(result);
        }

        /// <summary>
        /// Update FrontPageRecord.
        /// </summary>
        /// <param name="updateFrontPageRecordCommand"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize]
        [Produces("application/json", "application/xml", Type = typeof(UpdateFrontPageRecordCommand))]
        public async Task<IActionResult> Update(UpdateFrontPageRecordCommand updateFrontPageRecordCommand)
        {
            var result = await _mediator.Send(updateFrontPageRecordCommand);
            return ReturnFormattedResponse(result);
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="Code"></param>
        /// <returns></returns>
        [HttpDelete("{Code}")]
        [Authorize]
        public async Task<IActionResult> Delete(Guid Code)
        {
            var result = await _mediator.Send(new DeleteFrontPageRecordCommand { Code = Code });
            return ReturnFormattedResponse(result);
        }
    }
}