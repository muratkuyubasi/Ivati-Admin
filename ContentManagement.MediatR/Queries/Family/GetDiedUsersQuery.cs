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
    public class GetDiedUsersQuery : IRequest<ServiceResponse<DiedFamilyMemberPaginationDTO>>
    {
        public int Skip { get; set; }
        public int PageSize { get; set; }

        public int MemberId { get; set; }

        public string? Search { get; set; }

        public string? OrderBy { get; set; }
    }
}
