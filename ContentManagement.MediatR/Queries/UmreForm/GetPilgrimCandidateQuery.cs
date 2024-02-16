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
    public class GetPilgrimCandidateQuery : IRequest<ServiceResponse<UmrePaginationDto>>
    {
        public int Skip { get; set; } = 0;

        public int PageSize { get; set; } = 10;

        public string? Search { get; set; }

        public int? PeriodId { get; set; }
    }
}
