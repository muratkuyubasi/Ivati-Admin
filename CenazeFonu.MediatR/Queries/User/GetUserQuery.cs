using CenazeFonu.Data.Dto;
using MediatR;
using System;
using CenazeFonu.Helper;

namespace CenazeFonu.MediatR.Queries
{
    public class GetUserQuery : IRequest<ServiceResponse<UserDto>>
    {
        public Guid Id { get; set; }
    }
}
