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
    public class GetExecutivesPaginationQuery : IRequest<ServiceResponse<UserPaginationDTO>>
    {
        public int Skip { get; set; } = 0;
        public int PageSize { get; set; }
        public bool? IsActive { get; set; } = null;
        public bool? IsDeleted { get; set; } = null;
        public int FamilyId { get; set; }
        public string? Search { get; set; }
        public string? OrderBy { get; set; }
        public int CityId { get; set; }
    }
}
