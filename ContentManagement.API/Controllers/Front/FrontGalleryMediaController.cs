using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using ContentManagement.Data.Dto;
using ContentManagement.Helper;
using PT.MediatR.Commands;
using ContentManagement.MediatR.Queries;

namespace ContentManagement.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FrontGalleryMediaController : BaseController
    {
        IMediator _mediator;
        PathHelper _pathHelper;

        public FrontGalleryMediaController(IMediator mediator, PathHelper pathHelper)
        {
            _mediator = mediator;
            _pathHelper = pathHelper;
        }

        /// <summary>
        /// Get By Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("{Id}")]
        [Produces("application/json", "application/xml", Type = typeof(FrontGalleryMediaDto))]
        public async Task<IActionResult> Get(int Id)
        {
            var result = await _mediator.Send(new GetFrontGalleryMediaQuery { Id = Id });

            return ReturnFormattedResponse(result);
        }
        /// <summary>
        /// Get All List
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Produces("application/json", "application/xml", Type = typeof(List<FrontGalleryMediaDto>))]
        public async Task<IActionResult> GetList()
        {
            var result = await _mediator.Send(new GetAllFrontGalleryMediaQuery { });

            return ReturnFormattedResponse(result);
        }

        /// <summary>
        /// Create Add FrontGalleryMedia.
        /// </summary>
        /// <param name="addFrontGalleryMediaCommand"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [Produces("application/json", "application/xml", Type = typeof(AddFrontGalleryMediaCommand))]
        public async Task<IActionResult> Add(AddFrontGalleryMediaCommand addFrontGalleryMediaCommand)
        {
            if (addFrontGalleryMediaCommand.FormFile != null)
            {
                var directoryRoot = _pathHelper.wwwRootPath + _pathHelper.DocumentPath;
                if (!Directory.Exists(directoryRoot))
                    Directory.CreateDirectory(directoryRoot);

                addFrontGalleryMediaCommand.FileUrl = Path.Combine(directoryRoot, Guid.NewGuid() + Path.GetExtension(addFrontGalleryMediaCommand.FormFile.FileName));
            }

            var result = await _mediator.Send(addFrontGalleryMediaCommand);
            return ReturnFormattedResponse(result);
        }

        /// <summary>
        /// Update FrontGalleryMedia.
        /// </summary>
        /// <param name="updateFrontGalleryMediaCommand"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize]
        [Produces("application/json", "application/xml", Type = typeof(UpdateFrontGalleryMediaCommand))]
        public async Task<IActionResult> Update(UpdateFrontGalleryMediaCommand updateFrontGalleryMediaCommand)
        {
            if (updateFrontGalleryMediaCommand.FormFile != null)
            {
                var directoryRoot = _pathHelper.wwwRootPath + _pathHelper.DocumentPath;
                if (!Directory.Exists(directoryRoot))
                    Directory.CreateDirectory(directoryRoot);

                updateFrontGalleryMediaCommand.FileUrl = Path.Combine(directoryRoot, Guid.NewGuid() + Path.GetExtension(updateFrontGalleryMediaCommand.FormFile.FileName));
            }

            var result = await _mediator.Send(updateFrontGalleryMediaCommand);
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
            var result = await _mediator.Send(new DeleteFrontGalleryMediaCommand { Id = Id });
            return ReturnFormattedResponse(result);
        }
    }
}