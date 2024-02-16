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
    public class FrontGalleryRecordController : BaseController
    {
        IMediator _mediator;

        public FrontGalleryRecordController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get By Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("{Id}")]
        [Produces("application/json", "application/xml", Type = typeof(FrontGalleryRecordDto))]
        public async Task<IActionResult> Get(int Id)
        {
            var result = await _mediator.Send(new GetFrontGalleryRecordQuery { Id = Id });

            return ReturnFormattedResponse(result);
        }
        /// <summary>
        /// Get All List
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Produces("application/json", "application/xml", Type = typeof(List<FrontGalleryRecordDto>))]
        public async Task<IActionResult> GetList()
        {
            var result = await _mediator.Send(new GetAllFrontGalleryRecordQuery { });

            return ReturnFormattedResponse(result);
        }

        /// <summary>
        /// Create Add FrontGalleryRecord.
        /// </summary>
        /// <param name="addFrontGalleryRecordCommand"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [Produces("application/json", "application/xml", Type = typeof(AddFrontGalleryRecordCommand))]
        public async Task<IActionResult> Add(AddFrontGalleryRecordCommand addFrontGalleryRecordCommand)
        {
            var result = await _mediator.Send(addFrontGalleryRecordCommand);
            return ReturnFormattedResponse(result);
        }

        /// <summary>
        /// Update FrontGalleryRecord.
        /// </summary>
        /// <param name="updateFrontGalleryRecordCommand"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize]
        [Produces("application/json", "application/xml", Type = typeof(UpdateFrontGalleryRecordCommand))]
        public async Task<IActionResult> Update(UpdateFrontGalleryRecordCommand updateFrontGalleryRecordCommand)
        {
            var result = await _mediator.Send(updateFrontGalleryRecordCommand);
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
            var result = await _mediator.Send(new DeleteFrontGalleryRecordCommand { Id = Id });
            return ReturnFormattedResponse(result);
        }
    }
}