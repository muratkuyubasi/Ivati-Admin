using ContentManagement.Data.Dto;
using MediatR;
using System;
using System.Collections.Generic;

namespace ContentManagement.MediatR.Queries
{
    public class GetRoleUsersQuery : IRequest<List<UserRoleDto>>
    {
        public Guid RoleId { get; set; }
    }
}
