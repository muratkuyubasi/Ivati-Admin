using ContentManagement.Data.Dto;
using ContentManagement.Helper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentManagement.MediatR.Queries
{
    public class GetFamilyByMemberIdQuery : IRequest<ServiceResponse<FamilyDTO>>
    {
        public Guid FamilyId { get; set; }
        public int? ReferenceNumber { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
