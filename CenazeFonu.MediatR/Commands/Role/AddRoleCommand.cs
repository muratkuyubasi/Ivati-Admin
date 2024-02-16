using CenazeFonu.Data.Dto;
using MediatR;
using System.Collections.Generic;
using CenazeFonu.Helper;

namespace CenazeFonu.MediatR.Commands
{
    public class AddRoleCommand : IRequest<ServiceResponse<RoleDto>>
    {
        public string Name { get; set; }
        public List<RoleClaimDto> RoleClaims { get; set; }
    }
}
