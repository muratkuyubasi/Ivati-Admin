using ContentManagement.Data.Dto;
using ContentManagement.Helper;
using MediatR;
using System;
using System.Collections.Generic;

namespace ContentManagement.MediatR.Queries
{
    public class GetUsersNoteByUserIdQuery : IRequest<ServiceResponse<List<NoteDTO>>>
    {
        public Guid UserId { get; set; }
    }
}
