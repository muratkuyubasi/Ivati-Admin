using ContentManagement.Data.Dto;
using MediatR;
using System;
using ContentManagement.Helper;

namespace ContentManagement.MediatR.Commands
{
    public class DeleteUserCommand : IRequest<ServiceResponse<UserDto>>
    {
        public Guid Id { get; set; }
    }
}
