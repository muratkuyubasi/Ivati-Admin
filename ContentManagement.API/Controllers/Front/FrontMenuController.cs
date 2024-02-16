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
    public class FrontMenuController : BaseController
    {
        IMediator _mediator;

        public FrontMenuController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get By Id
        /// </summary>
        /// <param name="Code"></param>
        /// <returns></returns>
        [HttpGet("{Code}")]
        [Produces("application/json", "application/xml", Type = typeof(FrontMenuDto))]
        public async Task<IActionResult> Get(Guid Code)
        {
            var result = await _mediator.Send(new GetFrontMenuQuery { Code = Code });

            return ReturnFormattedResponse(result);
        }
        /// <summary>
        /// Get All List
        /// </summary>
        /// <returns></returns>
        [HttpGet("{languageCode}")]
        [Produces("application/json", "application/xml", Type = typeof(List<FrontMenuDto>))]
        public async Task<IActionResult> GetList(string languageCode)
        {
            var result = await _mediator.Send(new GetAllFrontMenuQuery { LanguageCode = languageCode});

            return ReturnFormattedResponse(result);
        }

        /// <summary>
        /// Create Add FrontMenu.
        /// </summary>
        /// <param name="addFrontMenuCommand"></param>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json", "application/xml", Type = typeof(AddFrontMenuCommand))]
        [Authorize]
        public async Task<IActionResult> Add(AddFrontMenuCommand addFrontMenuCommand)
        {
            var result = await _mediator.Send(addFrontMenuCommand);
            return ReturnFormattedResponse(result);
        }

        /// <summary>
        /// Update FrontMenu.
        /// </summary>
        /// <param name="updateFrontMenuCommand"></param>
        /// <returns></returns>
        [HttpPut]
        [Produces("application/json", "application/xml", Type = typeof(UpdateFrontMenuCommand))]
        [Authorize]
        public async Task<IActionResult> Update(UpdateFrontMenuCommand updateFrontMenuCommand)
        {
            var result = await _mediator.Send(updateFrontMenuCommand);
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
            var result = await _mediator.Send(new DeleteFrontMenuCommand { Code = Code });
            return Ok(result);
        }
    }
}