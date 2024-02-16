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
    public class FrontMenuRecordController : BaseController
    {
        IMediator _mediator;

        public FrontMenuRecordController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get By Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("{Id}")]
        [Produces("application/json", "application/xml", Type = typeof(FrontMenuRecordDto))]
        public async Task<IActionResult> Get(int Id)
        {
            var result = await _mediator.Send(new GetFrontMenuRecordQuery { Id = Id });

            return ReturnFormattedResponse(result);
        }
        /// <summary>
        /// Get All List
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Produces("application/json", "application/xml", Type = typeof(List<FrontMenuRecordDto>))]
        public async Task<IActionResult> GetList()
        {
            var result = await _mediator.Send(new GetAllFrontMenuRecordQuery { });

            return ReturnFormattedResponse(result);
        }

        /// <summary>
        /// Create Add FrontMenuRecord.
        /// </summary>
        /// <param name="addFrontMenuRecordCommand"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [Produces("application/json", "application/xml", Type = typeof(AddFrontMenuRecordCommand))]
        public async Task<IActionResult> Add(AddFrontMenuRecordCommand addFrontMenuRecordCommand)
        {
            var result = await _mediator.Send(addFrontMenuRecordCommand);
            return ReturnFormattedResponse(result);
        }

        /// <summary>
        /// Update FrontMenuRecord.
        /// </summary>
        /// <param name="updateFrontMenuRecordCommand"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize]
        [Produces("application/json", "application/xml", Type = typeof(UpdateFrontMenuRecordCommand))]
        public async Task<IActionResult> Update(UpdateFrontMenuRecordCommand updateFrontMenuRecordCommand)
        {
            var result = await _mediator.Send(updateFrontMenuRecordCommand);
            return ReturnFormattedResponse(result);
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("{Id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int Id)
        {
            var result = await _mediator.Send(new DeleteFrontMenuRecordCommand { Id = Id });
            return ReturnFormattedResponse(result);
        }
    }
}