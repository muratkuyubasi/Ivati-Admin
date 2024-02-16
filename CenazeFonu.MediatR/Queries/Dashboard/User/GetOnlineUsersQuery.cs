using MediatR;
using System.Collections.Generic;
using CenazeFonu.Data.Dto;

namespace CenazeFonu.MediatR.Queries
{
    public class GetOnlineUsersQuery : IRequest<List<UserDto>>
    {
    }
}
