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
    public class GetFamilyMembersPaginationQuery : IRequest<ServiceResponse<FamilyMemberPaginationDto>>
    {
        public int Skip { get; set; } = 0;
        public int PageSize { get; set; }
        public bool? IsActive { get; set; } = null;
        public bool? IsDeleted { get; set; } = null;
        public string? Search { get; set; }
        public string? OrderBy { get; set; }
        public int CityId { get; set; }
        public bool? Erkek21Yas { get; set; }
        public bool? Kadin23Yas { get; set; }

        public bool? AileFertlerineGoreSirala { get; set; }
    }
}
