using ContentManagement.Data.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using ContentManagement.Helper;

namespace ContentManagement.MediatR.Commands
{
    public class UpdateUserRoleCommand : IRequest<ServiceResponse<UserRoleDto>>
    {
        public Guid Id { get; set; }
        public List<UserRoleDto> UserRoles { get; set; }
    }
}
