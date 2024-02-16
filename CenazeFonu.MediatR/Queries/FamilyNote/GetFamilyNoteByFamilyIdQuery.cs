using CenazeFonu.Data.Dto;
using CenazeFonu.Helper;
using MediatR;
using System;
using System.Collections.Generic;

namespace CenazeFonu.MediatR.Queries
{
    public class GetFamilyNoteByFamilyIdQuery : IRequest<ServiceResponse<List<FamilyNoteDTO>>>
    {
        public Guid FamilyId { get; set; }
    }
}
