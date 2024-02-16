using ContentManagement.Data.Dto;
using MediatR;
using System.Collections.Generic;
using ContentManagement.Helper;

namespace ContentManagement.MediatR.Commands
{
    public class AddRoleCommand : IRequest<ServiceResponse<RoleDto>>
    {
        public string Name { get; set; }
        public List<RoleClaimDto> RoleClaims { get; set; }
    }
}
