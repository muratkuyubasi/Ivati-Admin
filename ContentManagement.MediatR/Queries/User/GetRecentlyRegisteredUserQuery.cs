using MediatR;
using System.Collections.Generic;
using ContentManagement.Data.Dto;

namespace ContentManagement.MediatR.Queries
{
    public class GetRecentlyRegisteredUserQuery : IRequest<List<UserDto>>
    {
    }
}
