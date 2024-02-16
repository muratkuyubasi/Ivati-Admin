using CenazeFonu.Data.Dto;
using MediatR;
using System;
using CenazeFonu.Helper;

namespace CenazeFonu.MediatR.Commands
{
    public class DeleteRoleCommand : IRequest<ServiceResponse<RoleDto>>
    {
        public Guid Id { get; set; }
    }
}
