using CenazeFonu.Data.Dto;
using MediatR;
using System;
using CenazeFonu.Helper;

namespace CenazeFonu.MediatR.Queries
{
    public class GetRoleQuery: IRequest<ServiceResponse<RoleDto>>
    {
        public Guid Id { get; set; }
    }
}
