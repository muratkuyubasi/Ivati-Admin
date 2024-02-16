//using CenazeFonu.MediatR.Commands;
//using CenazeFonu.MediatR.Queries;
//using MediatR;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using System.Collections.Generic;
//using System.Threading.Tasks;

//namespace CenazeFonu.API.Controllers.Fon
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class FonController : BaseController
//    {
//        private readonly IMediator _mediator;

//        public FonController(IMediator mediator)
//        {
//            _mediator = mediator;
//        }

//        [HttpPost("Create")]
//        public async Task<IActionResult> Create(AddFonCommand addFonCommand)
//        {
//            var result = await _mediator.Send(addFonCommand);
//            return ReturnFormattedResponse(result);
//        }
//    }
//}
