//using ContentManagement.MediatR.Handlers;
//using MediatR;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using System.Threading.Tasks;

//namespace ContentManagement.API.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class FamilyMemberController : BaseController
//    {
//        private readonly IMediator _mediator;

//        public FamilyMemberController(IMediator mediator)
//        {
//            _mediator = mediator;
//        }

//        ///<summary>Get Family Member</summary>
//        ///<return></return>
//        [HttpGet("FamilyMembers")]
//        public async Task<IActionResult> GetFamilyMembers()
//        {
//            var members = new GetFamilyMembersQuery();
//            var result = await _mediator.Send(members);
//            return ReturnFormattedResponse(result);
//        }
//    }

//}
