using ContentManagement.Data.Dto;
using ContentManagement.MediatR.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContentManagement.API.Controllers.Gender
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenderController : BaseController
    {
        private readonly IMediator _mediator;

        public GenderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        ///<summary>Get Genders</summary><return></return>
        [HttpGet("GetGenders")]
        [Produces("application/json", "application/xml", Type = typeof(List<GenderDTO>))]
        public async Task<IActionResult> GetGenders()
        {
            var data = new GetAllGendersQuery();
            var result = await _mediator.Send(data);
            return ReturnFormattedResponse(result);
        }
    }
}
