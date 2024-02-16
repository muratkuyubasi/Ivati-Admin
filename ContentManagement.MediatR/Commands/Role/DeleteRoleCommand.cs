using ContentManagement.Data.Dto;
using MediatR;
using System;
using ContentManagement.Helper;

namespace ContentManagement.MediatR.Commands
{
    public class DeleteRoleCommand : IRequest<ServiceResponse<RoleDto>>
    {
        public Guid Id { get; set; }
    }
}
