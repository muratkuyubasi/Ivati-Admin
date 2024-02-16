using ContentManagement.Data.Dto;
using ContentManagement.Helper;
using MediatR;
using System;
using System.Collections.Generic;

namespace ContentManagement.MediatR.Queries
{
    public class GetFamilyNoteByFamilyIdQuery : IRequest<ServiceResponse<List<FamilyNoteDTO>>>
    {
        public Guid FamilyId { get; set; }
    }
}
