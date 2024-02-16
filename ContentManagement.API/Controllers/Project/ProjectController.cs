using ContentManagement.Data.Dto;
using ContentManagement.API.Controllers;
using ContentManagement.MediatR.Commands;
using ContentManagement.MediatR.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace ContentManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : BaseController
    {
        private readonly IMediator _mediator;
        public ProjectController(IMediator mediator)
        {
            _mediator = mediator;
        }

        ///<summary> Get All Projects </summary><returns></returns>
        [HttpGet("GetAllProjects")]
        [Produces("application/json", Type = typeof(List<ProjectDTO>))]
        public async Task<IActionResult> GetAllProjects()
        {
            var data = new GetProjectsQuery { };
            var result = await _mediator.Send(data);
            return ReturnFormattedResponse(result);
        }

        ///<summary> Get Project By ID </summary><returns></returns>
        [HttpGet("GetProjectByID/{id}")]
        [Produces("application/json", Type = typeof(ProjectDTO))]
        public async Task<IActionResult> GetProjectByID(int id)
        {
            var data = new GetProjectByIdQuery { Id = id };
            var result = await _mediator.Send(data);
            return ReturnFormattedResponse(result);
        }

        ///<summary> Add Project </summary><returns></returns>
        [HttpPost("AddProject")]
        [Produces("application/json", Type = typeof(ProjectDTO))]
        [Authorize]
        public async Task<IActionResult> AddProject(AddProjectCommand command)
        {
            var result = await _mediator.Send(command);
            return ReturnFormattedResponse(result);
        }

        ///<summary> Update Project </summary><returns></returns>
        [HttpPut("UpdateProject")]
        [Produces("application/json", Type = typeof(ProjectDTO))]
        [Authorize]
        public async Task<IActionResult> UpdateProject(UpdateProjectCommand command)
        {
            var result = await _mediator.Send(command);
            return ReturnFormattedResponse(result);
        }

        ///<summary> Delete Project </summary><returns></returns>
        [HttpDelete("DeleteProject/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var data = new DeleteProjectCommand { Id = id };
            var result = await _mediator.Send(data);
            return ReturnFormattedResponse(result);
        }
    }
}
