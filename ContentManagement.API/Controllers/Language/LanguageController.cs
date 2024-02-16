using ContentManagement.Data.Dto;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using ContentManagement.API.Controllers;
using ContentManagement.MediatR.Commands;
using ContentManagement.MediatR.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContentManagement.API.Controllers.Language
{
    [Route("api/[controller]")]
    [ApiController]
    public class LanguageController : BaseController
    {
        private readonly IMediator _mediator;
        public LanguageController(IMediator mediator)
        {
            _mediator = mediator;
        }

        ///<summary> Get All Languages </summary><returns></returns>
        [HttpGet("GetAllLanguages")]
        [Produces("application/json", Type = typeof(List<LanguageDTO>))]
        public async Task<IActionResult> GetAllLanguages()
        {
            var data = new GetAllLanguagesQuery { };
            var result = await _mediator.Send(data);
            return ReturnFormattedResponse(result);
        }

        ///<summary> Get Language By ID </summary><returns></returns>
        [HttpGet("GetLanguageByID/{id}")]
        [Produces("application/json", Type = typeof(LanguageDTO))]
        public async Task<IActionResult> GetLanguageByID(int id)
        {
            var data = new GetLanguageByIdQuery { Id = id };
            var result = await _mediator.Send(data);
            return ReturnFormattedResponse(result);
        }

        ///<summary> Get Language By Langcode </summary><returns></returns>
        [HttpGet("GetLanguageByLangcode/{langcode}")]
        [Produces("application/json", Type = typeof(LanguageDTO))]
        public async Task<IActionResult> GetLanguageByID(string langcode)
        {
            var data = new GetLanguageByLangcodeQuery { Langcode = langcode };
            var result = await _mediator.Send(data);
            return ReturnFormattedResponse(result);
        }

        ///<summary> Add Language </summary><returns></returns>
        [HttpPost("AddLanguage")]
        [Produces("application/json", Type = typeof(LanguageDTO))]
        public async Task<IActionResult> AddLanguage(AddLanguageCommand command)
        {
            var result = await _mediator.Send(command);
            return ReturnFormattedResponse(result);
        }

        ///<summary> Update Language </summary><returns></returns>
        [HttpPut("UpdateLanguage")]
        [Produces("application/json", Type = typeof(LanguageDTO))]
        public async Task<IActionResult> UpdateLanguage(UpdateLanguageCommand command)
        {
            var result = await _mediator.Send(command);
            return ReturnFormattedResponse(result);
        }

        ///<summary> Delete Language </summary><returns></returns>
        [HttpDelete("DeleteLanguage/{id}")]
        public async Task<IActionResult> DeleteLanguage(int id)
        {
            var data = new DeleteLanguageCommand { Id = id };
            var result = await _mediator.Send(data);
            return ReturnFormattedResponse(result);
        }
    }
}
