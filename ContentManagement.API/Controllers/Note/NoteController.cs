using ContentManagement.Data.Dto;
using ContentManagement.MediatR.Commands;
using ContentManagement.MediatR.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContentManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NoteController : BaseController
    {
        private readonly IMediator _mediator;

        public NoteController(IMediator mediator)
        {
            _mediator = mediator;
        }

        ///<summary> Get All Family Notes </summary><returns></returns>
        [HttpGet("GetAllFamilyNotes")]
        [Produces("application/json", "application/xml", Type = typeof(List<FamilyNoteDTO>))]
        public async Task<IActionResult> GetAllFamilyNotes()
        {
            var data = new GetAllFamilyNotesQuery();
            var result = await _mediator.Send(data);
            return ReturnFormattedResponse(result);
        }

        ///<summary> Get Family Note By ID</summary><returns></returns>
        [HttpGet("GetFamilyNoteById")]
        [Produces("application/json", "application/xml", Type = typeof(FamilyNoteDTO))]
        public async Task<IActionResult> GetByIdF(int familyId)
        {
            var data = new GetFamilyNoteByIdQuery { Id = familyId };
            var result = await _mediator.Send(data);
            return ReturnFormattedResponse(result);
        }

        ///<summary> Get Families Notes By Family ID</summary><returns></returns>
        [HttpGet("GetByFamilyId")]
        [Produces("application/json", "application/xml", Type = typeof(List<FamilyNoteDTO>))]
        public async Task<IActionResult> GetByFamilyId(Guid id)
        {
            var data = new GetFamilyNoteByFamilyIdQuery { FamilyId = id };
            var result = await _mediator.Send(data);
            return ReturnFormattedResponse(result);
        }


        ///<summary> Get All Notes </summary><returns></returns>
        [HttpGet("GetAll")]
        [Produces("application/json", "application/xml", Type = typeof(NoteDTO))]
        public async Task<IActionResult> GetAll()
        {
            var data = new GetAllNotesQuery();
            var result = await _mediator.Send(data);
            return ReturnFormattedResponse(result);
        }

        ///<summary> Get Note By ID</summary><returns></returns>
        [HttpGet("GetById")]
        [Produces("application/json", "application/xml", Type = typeof(NoteDTO))]
        public async Task<IActionResult> GetById(int id)
        {
            var data = new GetNoteByIdQuery { Id = id};
            var result = await _mediator.Send(data);
            return ReturnFormattedResponse(result);
        }

        ///<summary> Get User's Notes By User ID</summary><returns></returns>
        [HttpGet("GetByUserId")]
        [Produces("application/json", "application/xml", Type = typeof(NoteDTO))]
        public async Task<IActionResult> GetByMinAndMaxAge(Guid id)
        {
            var data = new GetUsersNoteByUserIdQuery { UserId = id};
            var result = await _mediator.Send(data);
            return ReturnFormattedResponse(result);
        }

        ///<summary> Add Note </summary><returns></returns>
        [HttpPost("Add")]
        [Produces("application/json", "application/xml", Type = typeof(NoteDTO))]
        public async Task<IActionResult> Add(AddNoteCommand command)
        {
            var data = await _mediator.Send(command);
            return Ok(data);
        }

        ///<summary> Update Note </summary><returns></returns>
        [HttpPut("Update")]
        [Produces("application/json", "application/xml", Type = typeof(NoteDTO))]
        public async Task<IActionResult> Update(UpdateNoteCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        ///<summary> Delete Note </summary><returns></returns>
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var data = new DeleteNoteCommand { Id = id };
            var result = await _mediator.Send(data);
            return Ok(result);
        }


        ///<summary> Add Family Note </summary><returns></returns>
        [HttpPost("AddFamilyNote")]
        //[Authorize]
        [Produces("application/json", "application/xml", Type = typeof(FamilyNoteDTO))]
        public async Task<IActionResult> AddFamilyNote(AddFamilyNoteCommand command)
        {
            var data = await _mediator.Send(command);
            return Ok(data);
        }

        ///<summary> Update Family Note </summary><returns></returns>
        [HttpPut("UpdateFamilyNote")]
        [Authorize]
        [Produces("application/json", "application/xml", Type = typeof(FamilyNoteDTO))]
        public async Task<IActionResult> UpdateFamilyNote(UpdateFamilyNoteCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        ///<summary> Delete Family Note </summary><returns></returns>
        [HttpDelete("DeleteFamilyNote")]
        [Authorize]
        public async Task<IActionResult> Family(int id)
        {
            var data = new DeleteFamilyNoteCommand { Id = id };
            var result = await _mediator.Send(data);
            return Ok(result);
        }
    }
}
