using ContentManagement.Data.Dto;
using ContentManagement.API.Controllers;
using ContentManagement.MediatR.Commands;
using ContentManagement.MediatR.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContentManagement.API.Controllers.Translation
{
    [Route("api/[controller]")]
    [ApiController]
    public class TranslationController : BaseController
    {
        private readonly IMediator _mediator;
        public TranslationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        ///<summary> Get All Translations </summary><returns></returns>
        [HttpGet("GetAllTranslations")]
        [Produces("application/json", Type = typeof(List<TranslationDTO>))]
        public async Task<IActionResult> GetAllTranslations()
        {
            var data = new GetTranslationsQuery { };
            var result = await _mediator.Send(data);
            return ReturnFormattedResponse(result);
        }

        ///<summary> Get Translation By ID </summary><returns></returns>
        [HttpGet("GetTranslationByID/{id}")]
        [Produces("application/json", Type = typeof(TranslationDTO))]
        public async Task<IActionResult> GetTranslationByID(int id)
        {
            var data = new GetTranslationByIdQuery { Id = id };
            var result = await _mediator.Send(data);
            return ReturnFormattedResponse(result);
        }

        ///<summary> Add Translation </summary><returns></returns>
        [HttpPost("AddTranslation")]
        [Produces("application/json", Type = typeof(TranslationDTO))]
        public async Task<IActionResult> AddTranslation(AddTranslationCommand command)
        {
            var result = await _mediator.Send(command);
            return ReturnFormattedResponse(result);
        }

        ///<summary> Update Translation </summary><returns></returns>
        [HttpPut("UpdateTranslation")]
        [Produces("application/json", Type = typeof(TranslationDTO))]
        public async Task<IActionResult> UpdateTranslation(UpdateTranslationCommand command)
        {
            var result = await _mediator.Send(command);
            return ReturnFormattedResponse(result);
        }

        ///<summary> Delete Translation </summary><returns></returns>
        [HttpDelete("DeleteTranslation/{id}")]
        public async Task<IActionResult> DeleteTranslation(int id)
        {
            var data = new DeleteTranslationCommand { Id = id };
            var result = await _mediator.Send(data);
            return ReturnFormattedResponse(result);
        }
    }
}
