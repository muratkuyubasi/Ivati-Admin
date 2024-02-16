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
    public class GetAllDebtorsPaginationQuery : IRequest<ServiceResponse<FamilyDebtorPaginationDto>>
    {
        public int Skip { get; set; } = 0;
        public int PageSize { get; set; }
        public int Year { get; set; }
        public bool? IsPayment { get; set; } = null;
        //public int FamilyNo { get; set; }
        //public string BillNo { get; set; }

        public string Search { get; set; }

        //public string OrderBy { get; set; }
    }
}
