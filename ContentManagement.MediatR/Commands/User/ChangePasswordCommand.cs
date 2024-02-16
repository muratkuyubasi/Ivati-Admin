using ContentManagement.Data.Dto;
using MediatR;
using ContentManagement.Helper;

namespace ContentManagement.MediatR.Commands
{
    public class ChangePasswordCommand : IRequest<ServiceResponse<UserDto>>
    {
        public string UserName { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
