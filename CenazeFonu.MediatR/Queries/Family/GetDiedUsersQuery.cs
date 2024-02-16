using CenazeFonu.Data.Dto;
using CenazeFonu.Helper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CenazeFonu.MediatR.Queries
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
