using CenazeFonu.Data.Dto;
using MediatR;
using System.Collections.Generic;

namespace CenazeFonu.MediatR.Queries
{
    public class GetAllRoleQuery : IRequest<List<RoleDto>>
    {
    }
}
