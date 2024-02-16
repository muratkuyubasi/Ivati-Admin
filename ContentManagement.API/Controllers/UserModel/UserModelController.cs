//using ContentManagement.MediatR.Commands;
//using ContentManagement.MediatR.Queries;
//using MediatR;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Threading.Tasks;

//namespace ContentManagement.API.Controllers.UserModel
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class UserModelController : BaseController
//    {
//        private readonly IMediator _mediator;

//        public UserModelController(IMediator mediator)
//        {
//            _mediator = mediator;
//        }

//        ///<summary>Get All Users Detail </summary>
//        [HttpGet("GetUsersDetail")]
//        public async Task<IActionResult> GetUsersDetail()
//        {
//            var data = new GetAllUserDetailQuery();
//            var result = await _mediator.Send(data);
//            return ReturnFormattedResponse(result);
//        }
//        /// <summary>
//        /// Add User
//        /// </summary>
//        /// <param name="userModelCommand"></param>
//        /// <returns></returns>
//        [HttpPost("Create")]
//        public async Task<IActionResult> Create(AddUserModelCommand userModelCommand)
//        {
//            var result = await _mediator.Send(userModelCommand);
//            return ReturnFormattedResponse(result);
//        }

//        /// <summary>
//        /// Get User Detail By Id
//        /// </summary>
//        /// <returns></returns>
//        [HttpGet("GetById")]
//        public async Task<IActionResult> GetById(Guid id)
//        {
//            var data = new GetUserDetailByIdQuery { Id = id };
//            var result = await _mediator.Send(data);
//            return ReturnFormattedResponse(result);
//        }
//    }
//}
