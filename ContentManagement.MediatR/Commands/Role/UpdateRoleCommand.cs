using ContentManagement.Data.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using ContentManagement.Helper;

namespace ContentManagement.MediatR.Commands
{
    public class UpdateRoleCommand: IRequest<ServiceResponse<RoleDto>>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<RoleClaimDto> RoleClaims { get; set; }
    }
}
