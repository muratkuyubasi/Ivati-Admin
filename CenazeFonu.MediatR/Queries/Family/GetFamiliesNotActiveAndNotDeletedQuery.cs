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
    public class GetFamiliesNotActiveAndNotDeletedQuery : IRequest<ServiceResponse<FamilyPaginationDto>>
    {
        public int Skip { get; set; }
        public int PageSize { get; set; }
    }
}
