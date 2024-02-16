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
    public class FrontPageController : BaseController
    {
        IMediator _mediator;

        public FrontPageController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get By Id
        /// </summary>
        /// <param name="Code"></param>
        /// <returns></returns>
        [HttpGet("{Code}")]
        [Produces("application/json", "application/xml", Type = typeof(FrontPageDto))]
        public async Task<IActionResult> Get(Guid Code)
        {
            var result = await _mediator.Send(new GetFrontPageQuery { Code = Code });

            return ReturnFormattedResponse(result);
        }
        /// <summary>
        /// Get All List
        /// </summary>
        /// <returns></returns>
        [HttpGet("{languageCode}")]
        [Produces("application/json", "application/xml", Type = typeof(List<FrontPageDto>))]
        public async Task<IActionResult> GetList(string languageCode)
        {
            var result = await _mediator.Send(new GetAllFrontPageQuery { LanguageCode = languageCode });

            return ReturnFormattedResponse(result);
        }

        /// <summary>
        /// Create Add FrontPage.
        /// </summary>
        /// <param name="addFrontPageCommand"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [Produces("application/json", "application/xml", Type = typeof(AddFrontPageCommand))]
        public async Task<IActionResult> Add(AddFrontPageCommand addFrontPageCommand)
        {
            var result = await _mediator.Send(addFrontPageCommand);
            return ReturnFormattedResponse(result);
        }

        /// <summary>
        /// Update FrontPage.
        /// </summary>
        /// <param name="updateFrontPageCommand"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize]
        [Produces("application/json", "application/xml", Type = typeof(UpdateFrontPageCommand))]
        public async Task<IActionResult> Update(UpdateFrontPageCommand updateFrontPageCommand)
        {
            var result = await _mediator.Send(updateFrontPageCommand);
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
            var result = await _mediator.Send(new DeleteFrontPageCommand { Code = Code });
            return ReturnFormattedResponse(result);
        }
    }
}