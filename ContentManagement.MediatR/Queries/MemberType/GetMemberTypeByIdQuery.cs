using ContentManagement.Data.Models;
using ContentManagement.Helper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentManagement.MediatR.Queries
{
    public class GetMemberTypeByIdQuery : IRequest<ServiceResponse<MemberTypeDTO>>
    {
        public int Id { get; set; }
    }
}
