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
    public class AddExecutiveCommand : IRequest<ServiceResponse<ExecutiveDTO>>
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
        public List<int?> CityId { get; set; }
    }
}
