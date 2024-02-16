using CenazeFonu.Data.Dto;
using CenazeFonu.Helper;
using MediatR;
using System;
using System.Collections.Generic;

namespace CenazeFonu.MediatR.Queries
{
    public class GetUsersNoteByUserIdQuery : IRequest<ServiceResponse<List<NoteDTO>>>
    {
        public Guid UserId { get; set; }
    }
}
