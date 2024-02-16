using ContentManagement.Data;
using ContentManagement.Data.Dto;
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
    public class GetChairmanByIdQuery : IRequest<ServiceResponse<ChairmanDTO>>
    {
        public int Id { get; set; }
    }
}
