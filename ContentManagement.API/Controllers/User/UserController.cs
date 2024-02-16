using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ContentManagement.Data.Dto;
using ContentManagement.MediatR.Commands;
using ContentManagement.MediatR.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using ContentManagement.Data.Resources;
using ContentManagement.Repository;
using ContentManagement.MediatR.Commands.User;
using ContentManagement.MediatR.Command;
using ContentManagement.MediatR.Queries.User;

namespace ContentManagement.API.Controllers
{
    /// <summary>
    /// User
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class UserController : BaseController
    {
        public IMediator _mediator { get; set; }
        private IWebHostEnvironment _webHostEnvironment;
        public readonly UserInfoToken _userInfo;
        /// <summary>
        /// User
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="userInfo"></param>
        public UserController(
            IMediator mediator,
            UserInfoToken userInfo,
            IWebHostEnvironment webHostEnvironment
            )
        {
            _mediator = mediator;
            _userInfo = userInfo;
            _webHostEnvironment = webHostEnvironment;
        }

        ///<summary>Get All Users Detail </summary>
        [HttpGet("GetUsersDetail")]
        [Produces("application/json", "application/xml", Type = typeof(List<UserDetailDTO>))]
        public async Task<IActionResult> GetUsersDetail()
        {
            var data = new GetAllUserDetailQuery();
            var result = await _mediator.Send(data);
            return ReturnFormattedResponse(result);
        }

        ///<summary>Get Users Pagination </summary>
        ///<return></return>
        [HttpGet("GetUsersPagination/{skip}/{pageSize}")]
        [Produces("application/json", "application/xml", Type = typeof(UserPaginationDTO))]
        [AllowAnonymous]
        public async Task<IActionResult> GetUsersPagination(int skip, int pageSize, bool? isActive, bool? isDeleted, int familyId, string? search, string? orderBy, int cityId, int roleId)
        {
            var datas = new GetAllUsersPaginationQuery
            {
                Skip = skip,
                PageSize = pageSize,
                IsActive = isActive,
                IsDeleted = isDeleted,
                FamilyId = familyId,
                Search = search,
                OrderBy = orderBy,
                CityId = cityId,
                RoleId = roleId
            };
            var result = await _mediator.Send(datas);
            if (result.StatusCode == 204)
            {
                return ReturnFormattedResponse(result);
            }
            if (result.Errors.Count > 0)
            {
                return Ok(result);
            }
            //Response.Headers.Add("X-Pagination",
            //    Newtonsoft.Json.JsonConvert.SerializeObject(paginationMetadata));
            return Ok(result);
        }


        [HttpGet("GetUserCountByCityID/{cityId}")]
        [Produces("application/json", "application/xml", Type = typeof(Object))]
        public async Task<IActionResult> GetUserCountByCityID(int cityId)
        {
            var data = new GetUserCountByCityIdQuery { CityId = cityId };
            var result = await _mediator.Send(data);
            return Ok(result);
        }

        [HttpGet("GetAllUserCount")]
        [Produces("application/json", "application/xml", Type = typeof(int))]
        public async Task<IActionResult> GetAllUserCount()
        {
            var data = new GetAllUserCountQuery();
            var result = await _mediator.Send(data);
            return ReturnFormattedResponse(result);
        }

        ///<summary>Get User Detail By Id</summary>
        [HttpGet("GetUserDetailByUserId")]
        [Produces("application/json", "application/xml", Type = typeof(UserDetailDTO))]
        public async Task<IActionResult> GetUserDetailByUserId(Guid Id)
        {
            var data = new GetUserDetailByIdQuery { Id = Id};
            var result = await _mediator.Send(data);
            return ReturnFormattedResponse(result);
        }

        /// <summary>
        ///  Create a User
        /// </summary>
        /// <param name="addUserCommand"></param>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json", "application/xml", Type = typeof(UserDto))]
        public async Task<IActionResult> AddUser(AddUserCommand addUserCommand)
        {
            var result = await _mediator.Send(addUserCommand);
            if (!result.Success)
            {
                return ReturnFormattedResponse(result);
            }
            return CreatedAtAction("GetUser", new { id = result.Data.Id }, result.Data);
        }

        /// <summary>
        /// Update User Contact Information
        /// </summary>
        /// <returns></returns>
        [HttpPut("UpdateUserContactInformation")]
        [Produces("application/json", "application/xml", Type = typeof(UpdateUserContactDTO))]
        public async Task<IActionResult> UpdateUserContactInformation(UpdateUserInformationCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }

        /// <summary>
        /// Update All User's Password
        /// </summary>
        /// <returns></returns>
        [HttpPut("UpdateAllUsersPassword")]
        [Produces("application/json", "application/xml", Type = typeof(bool))]
        public async Task<IActionResult> UpdateAllUsersPassword(ResetAllUsersPasswordCommand command)
        {
            var result = await _mediator.Send(command);
            return ReturnFormattedResponse(result);
        }


        /// <summary>
        /// Get All Users
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllUsers")]
        [Produces("application/json", "application/xml", Type = typeof(List<UserDto>))]
        public async Task<IActionResult> GetAllUsers()
        {
            var getAllUserQuery = new GetAllUserQuery { };
            var result = await _mediator.Send(getAllUserQuery);
            return Ok(result);
        }

        /// <summary>
        /// Get User By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetUser")]
        [Produces("application/json", "application/xml", Type = typeof(UserDto))]
        public async Task<IActionResult> GetUser(Guid id)
        {
            var getUserQuery = new GetUserQuery { Id = id };
            var result = await _mediator.Send(getUserQuery);
            return ReturnFormattedResponse(result);
        }

        /// <summary>
        /// Get Users
        /// </summary>
        /// <param name="userResource"></param>
        /// <returns></returns>
        [HttpGet("GetUsers")]
        [Produces("application/json", "application/xml", Type = typeof(UserList))]
        public async Task<IActionResult> GetUsers([FromQuery] UserResource userResource)
        {
            var getAllLoginAuditQuery = new GetUsersQuery
            {
                UserResource = userResource
            };
            var result = await _mediator.Send(getAllLoginAuditQuery);

            var paginationMetadata = new
            {
                totalCount = result.TotalCount,
                pageSize = result.PageSize,
                skip = result.Skip,
                totalPages = result.TotalPages
            };
            Response.Headers.Add("X-Pagination",
                Newtonsoft.Json.JsonConvert.SerializeObject(paginationMetadata));
            return Ok(result);
        }

        /// <summary>
        /// Get Recently Registered Users
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetRecentlyRegisteredUsers")]
        [Produces("application/json", "application/xml", Type = typeof(List<UserDto>))]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> GetRecentlyRegisteredUsers()
        {
            var getRecentlyRegisteredUserQuery = new GetRecentlyRegisteredUserQuery { };
            var result = await _mediator.Send(getRecentlyRegisteredUserQuery);
            return Ok(result);
        }


        /// <summary>
        /// User Login
        /// </summary>
        /// <param name="userLoginCommand"></param>
        /// <returns></returns>
        [HttpPost("login")]
        [AllowAnonymous]
        [Produces("application/json", "application/xml", Type = typeof(UserAuthDto))]
        public async Task<IActionResult> UserLogin(UserLoginCommand userLoginCommand)
        {
            userLoginCommand.RemoteIp = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            var result = await _mediator.Send(userLoginCommand);

            if (!result.Success)
            {
                return ReturnFormattedResponse(result);
            }
            if (!string.IsNullOrWhiteSpace(result.Data.ProfilePhoto))
            {
                result.Data.ProfilePhoto = $"Users/{result.Data.ProfilePhoto}";
            }

            return Ok(result.Data);
        }

        /// <summary>
        /// Update User By Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateUserCommand"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json", "application/xml", Type = typeof(UserDto))]
        [ApiExplorerSettings(IgnoreApi = false)]
        public async Task<IActionResult> UpdateUser(Guid id, UpdateUserCommand updateUserCommand)
        {
            updateUserCommand.Id = id;
            var result = await _mediator.Send(updateUserCommand);
            return ReturnFormattedResponse(result);
        }

        /// <summary>
        /// Update Profile
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateUserProfileCommand"></param>
        /// <returns></returns>
        [HttpPut("profile")]
        [Produces("application/json", "application/xml", Type = typeof(UserDto))]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> UpdateUserProfile(UpdateUserProfileCommand updateUserProfileCommand)
        {
            var result = await _mediator.Send(updateUserProfileCommand);
            return ReturnFormattedResponse(result);
        }

        /// <summary>
        /// Update Profile photo
        /// </summary>
        /// <returns></returns>
        [HttpPost("UpdateUserProfilePhoto"), DisableRequestSizeLimit]
        [Produces("application/json", "application/xml", Type = typeof(UserDto))]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> UpdateUserProfilePhoto()
        {
            var updateUserProfilePhotoCommand = new UpdateUserProfilePhotoCommand()
            {
                FormFile = Request.Form.Files,
                RootPath = _webHostEnvironment.WebRootPath
            };
            var result = await _mediator.Send(updateUserProfilePhotoCommand);
            return ReturnFormattedResponse(result);
        }

        /// <summary>
        /// Delete User By Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("{Id}")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> DeleteUser(Guid Id)
        {
            var deleteUserCommand = new DeleteUserCommand { Id = Id };
            var result = await _mediator.Send(deleteUserCommand);
            return ReturnFormattedResponse(result);
        }

        /// <summary>
        /// User Change Password
        /// </summary>
        /// <param name="resetPasswordCommand"></param>
        /// <returns></returns>
        [HttpPost("changepassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordCommand resetPasswordCommand)
        {
            var result = await _mediator.Send(resetPasswordCommand);
            return ReturnFormattedResponse(result);
        }

        /// <summary>
        /// Reset Resetpassword
        /// </summary>
        /// <param name="newPasswordCommand"></param>
        /// <returns></returns>
        [HttpPost("resetpassword")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPasswordCommand newPasswordCommand)
        {
            var result = await _mediator.Send(newPasswordCommand);
            return ReturnFormattedResponse(result);
        }

        /// <summary>
        /// Get User Profile
        /// </summary>
        /// <returns></returns>
        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            var getUserQuery = new GetUserQuery
            {
                Id = Guid.Parse(_userInfo.Id)
            };
            var result = await _mediator.Send(getUserQuery);
            if (!string.IsNullOrWhiteSpace(result.Data.ProfilePhoto))
            {
                result.Data.ProfilePhoto = $"Users/{result.Data.ProfilePhoto}";
            }
            return ReturnFormattedResponse(result);
        }

        [HttpPut("UpdateWifesIdentificationNumber")]
        public async Task<IActionResult> SetWifeIdentificationNumber()
        {
            var data = new SetWifesIdentificationNumberCommand();
            var reuslt = await _mediator.Send(data);
            return Ok(reuslt);
        }
    }
}
