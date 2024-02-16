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
    public class GetUserDetailByIdQuery : IRequest<ServiceResponse<UserDetailDTO>>
    {
        public Guid Id { get; set; }
    }
}
