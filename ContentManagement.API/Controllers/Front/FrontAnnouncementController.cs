using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ContentManagement.Data.Dto;
using PT.MediatR.Commands;
using ContentManagement.MediatR.Queries;
using ContentManagement.API.Controllers;
using Microsoft.AspNetCore.Hosting;
using ContentManagement.MediatR.Commands;

namespace ContentManagement.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FrontAnnouncementController : BaseController
    {
        IMediator _mediator;
        private IWebHostEnvironment _webHostEnvironment;

        public FrontAnnouncementController(IMediator mediator,
            IWebHostEnvironment webHostEnvironment)
        {
            _mediator = mediator;
            _webHostEnvironment = webHostEnvironment;
        }

        /// <summary>
        /// Get By Id
        /// </summary>
        /// <param name="Code"></param>
        /// <returns></returns>
        [HttpGet("{Code}")]
        [Produces("application/json", "application/xml", Type = typeof(FrontAnnouncementDto))]
        public async Task<IActionResult> Get(Guid Code)
        {
            var result = await _mediator.Send(new GetFrontAnnouncementQuery { Code = Code });

            return ReturnFormattedResponse(result);
        }
        /// <summary>
        /// Get All List
        /// </summary>
        /// <returns></returns>
        [HttpGet("{languageCode}")]
        [Produces("application/json", "application/xml", Type = typeof(List<FrontAnnouncementDto>))]
        public async Task<IActionResult> GetList(string languageCode)
        {
            var result = await _mediator.Send(new GetAllFrontAnnouncementQuery { LanguageCode = languageCode });

            return ReturnFormattedResponse(result);
        }

        /// <summary>
        /// Create Add FrontAnnouncement.
        /// </summary>
        /// <param name="addFrontAnnouncementCommand"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [Produces("application/json", "application/xml", Type = typeof(AddFrontAnnouncementCommand))]
        public async Task<IActionResult> Add(AddFrontAnnouncementCommand addFrontAnnouncementCommand)
        {
            var result = await _mediator.Send(addFrontAnnouncementCommand);
            return ReturnFormattedResponse(result);
        }

        /// <summary>
        /// Update FrontAnnouncement.
        /// </summary>
        /// <param name="updateFrontAnnouncementCommand"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize]
        [Produces("application/json", "application/xml", Type = typeof(UpdateFrontAnnouncementCommand))]
        public async Task<IActionResult> Update(UpdateFrontAnnouncementCommand updateFrontAnnouncementCommand)
        {
            var result = await _mediator.Send(updateFrontAnnouncementCommand);
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
            var result = await _mediator.Send(new DeleteFrontAnnouncementCommand { Code = Code });
            return ReturnFormattedResponse(result);
        }


        /// <summary>
        /// Upload Announcemenet photo
        /// </summary>
        /// <returns></returns>
        [HttpPost("UploadPhoto"), DisableRequestSizeLimit]
        [Authorize]
        [Produces("application/json", "application/xml", Type = typeof(string))]
        //[ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> UploadPhoto([FromForm] UploadPhotoCommand command)
        {
            var uploadCommand = new UploadPhotoCommand()
            {
                FormFile = Request.Form.Files,
                RootPath = _webHostEnvironment.WebRootPath,
                Width = command.Width,
                Height = command.Height
            };
            var result = await _mediator.Send(uploadCommand);
            return Ok(result);
        }
    }
}