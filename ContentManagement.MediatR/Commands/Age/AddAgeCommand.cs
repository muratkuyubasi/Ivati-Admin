using ContentManagement.Data.Dto;
using ContentManagement.Helper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentManagement.MediatR.Commands
{
    public class AddAgeCommand : IRequest<ServiceResponse<AgeDTO>>
    {
        public int MaxAge { get; set; }
        public int MinAge { get; set; }
        public decimal? EntranceFree { get; set; }
        public decimal? Dues { get; set; }
    }
}
