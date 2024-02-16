using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ContentManagement.Data.Dto;
using PT.MediatR.Commands;
using ContentManagement.MediatR.Queries;
using ContentManagement.MediatR.Commands;

namespace ContentManagement.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FrontAnnouncementRecordController : BaseController
    {
        IMediator _mediator;

        public FrontAnnouncementRecordController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get By Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("{Id}")]
        [Produces("application/json", "application/xml", Type = typeof(FrontAnnouncementRecordDto))]
        public async Task<IActionResult> Get(int Id)
        {
            var result = await _mediator.Send(new GetFrontAnnouncementRecordQuery { Id = Id });

            return ReturnFormattedResponse(result);
        }
        /// <summary>
        /// Get All List
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Produces("application/json", "application/xml", Type = typeof(List<FrontAnnouncementRecordDto>))]
        public async Task<IActionResult> GetList()
        {
            var result = await _mediator.Send(new GetAllFrontAnnouncementRecordQuery { });

            return ReturnFormattedResponse(result);
        }

        /// <summary>
        /// Create Add FrontAnnouncementRecord.
        /// </summary>
        /// <param name="addFrontAnnouncementRecordCommand"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [Produces("application/json", "application/xml", Type = typeof(AddFrontAnnouncementRecordCommand))]
        public async Task<IActionResult> Add(AddFrontAnnouncementRecordCommand addFrontAnnouncementRecordCommand)
        {
            var result = await _mediator.Send(addFrontAnnouncementRecordCommand);
            return ReturnFormattedResponse(result);
        }

        /// <summary>
        /// Update FrontAnnouncementRecord.
        /// </summary>
        /// <param name="updateFrontAnnouncementRecordCommand"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize]
        [Produces("application/json", "application/xml", Type = typeof(UpdateFrontAnnouncementRecordCommand))]
        public async Task<IActionResult> Update(UpdateFrontAnnouncementRecordCommand updateFrontAnnouncementRecordCommand)
        {
            var result = await _mediator.Send(updateFrontAnnouncementRecordCommand);
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
            var result = await _mediator.Send(new DeleteFrontAnnouncementRecordCommand { Id = Id });
            return ReturnFormattedResponse(result);
        }
    }
}