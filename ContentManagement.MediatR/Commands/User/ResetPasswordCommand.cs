using ContentManagement.Data.Dto;
using MediatR;
using ContentManagement.Helper;

namespace ContentManagement.MediatR.Commands
{
    public class ResetPasswordCommand : IRequest<ServiceResponse<UserDto>>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
