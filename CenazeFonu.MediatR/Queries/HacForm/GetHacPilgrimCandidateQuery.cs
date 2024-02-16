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
    public class GetHacPilgrimCandidateQuery : IRequest<ServiceResponse<HacPaginationDto>>
    {
        public int Skip { get; set; } = 0;

        public int PageSize { get; set; } = 10;

        public string? Search { get; set; }

        public int? PeriodId { get; set; }
    }
}
