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
    public class FrontGalleryController : BaseController
    {
        IMediator _mediator;

        public FrontGalleryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get By Id
        /// </summary>
        /// <param name="Code"></param>
        /// <returns></returns>
        [HttpGet("{Code}")]
        [Produces("application/json", "application/xml", Type = typeof(FrontGalleryDto))]
        public async Task<IActionResult> Get(Guid Code)
        {
            var result = await _mediator.Send(new GetFrontGalleryQuery { Code = Code });

            return ReturnFormattedResponse(result);
        }
        /// <summary>
        /// Get All List
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Produces("application/json", "application/xml", Type = typeof(List<FrontGalleryDto>))]
        public async Task<IActionResult> GetList()
        {
            var result = await _mediator.Send(new GetAllFrontGalleryQuery { });

            return ReturnFormattedResponse(result);
        }

        /// <summary>
        /// Create Add FrontGallery.
        /// </summary>
        /// <param name="addFrontGalleryCommand"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [Produces("application/json", "application/xml", Type = typeof(AddFrontGalleryCommand))]
        public async Task<IActionResult> Add(AddFrontGalleryCommand addFrontGalleryCommand)
        {
            var result = await _mediator.Send(addFrontGalleryCommand);
            return ReturnFormattedResponse(result);
        }

        /// <summary>
        /// Update FrontGallery.
        /// </summary>
        /// <param name="updateFrontGalleryCommand"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize]
        [Produces("application/json", "application/xml", Type = typeof(UpdateFrontGalleryCommand))]
        public async Task<IActionResult> Update(UpdateFrontGalleryCommand updateFrontGalleryCommand)
        {
            var result = await _mediator.Send(updateFrontGalleryCommand);
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
            var result = await _mediator.Send(new DeleteFrontGalleryCommand { Code = Code });
            return ReturnFormattedResponse(result);
        }
    }
}