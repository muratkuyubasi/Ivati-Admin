using ContentManagement.Data.Dto;
using ContentManagement.MediatR.Commands;
using ContentManagement.MediatR.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ContentManagement.API.Controllers.Executive
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ExecutiveController : BaseController
    {
        private readonly IMediator _mediator;

        public ExecutiveController(IMediator mediator)
        {
            _mediator = mediator;
        }


        ///<summary>Get Executives Pagination </summary>
        ///<return></return>
        [HttpGet("GetExecutivesPagination/{skip}/{pageSize}")]
        [Produces("application/json", "application/xml", Type = typeof(ExecutivePaginationDto))]
        [AllowAnonymous]
        public async Task<IActionResult> GetExecutivesPagination(int skip, int pageSize, bool? isActive, bool? isDeleted, int memberId, string? search, string? orderBy)
        {
            var datas = new GetAllExecutivesPaginationQuery
            {
                Skip = skip,
                PageSize = pageSize,
                IsActive = isActive,
                IsDeleted = isDeleted,
                MemberId = memberId,
                Search = search,
                OrderBy = orderBy
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
            var paginationMetadata = new
            {
                TotalCount = result.Data.TotalCount,
                Skip = result.Data.Skip,
                PageSize = result.Data.PageSize,
                Data = result.Data
            };
            //Response.Headers.Add("X-Pagination",
            //    Newtonsoft.Json.JsonConvert.SerializeObject(paginationMetadata));
            return Ok(paginationMetadata);
        }

        ///<summary>Get Executive By ID</summary><returns></returns>
        [HttpGet("GetExecutiveByID/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(Guid id)
        {
            var data = new GetExecutiveByIDQuery { Id = id };
            var result = await _mediator.Send(data);
            return ReturnFormattedResponse(result);
        }

        ///<summary>Get Executive By User ID</summary><returns></returns>
        [HttpGet("GetExecutiveByUserID/{userId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetExecutiveByUserID(Guid userId)
        {
            var data = new GetExecutiveDetailByUserIDQuery { UserId = userId };
            var result = await _mediator.Send(data);
            return ReturnFormattedResponse(result);
        }
        ///<summary>Add Executive</summary><returns></returns>
        [HttpPost("AddExecutive")]
        public async Task<IActionResult> AddExecutive(AddExecutiveCommand addExecutiveCommand)
        {
            var result = await _mediator.Send(addExecutiveCommand);
            return ReturnFormattedResponse(result);
        }

        ///<summary>Update Executive</summary><returns></returns>
        [HttpPut("UpdateExecutive")]
        public async Task<IActionResult> UpdateExecutive(UpdateExecutiveCommand command)
        {
            var result = await _mediator.Send(command);
            return ReturnFormattedResponse(result);
        }

        ///<summary>Delete Executive By ID</summary><returns></returns>
        [HttpDelete("DeleteExecutive/{id}")]
        public async Task<IActionResult> DeleteExecutive(Guid id)
        {
            var data = new DeleteExecutiveCommand { Id = id };
            var result = await _mediator.Send(data);
            return ReturnFormattedResponse(result);
        }
    }
}
