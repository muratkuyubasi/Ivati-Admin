using CenazeFonu.Data.Dto;
using MediatR;
using CenazeFonu.Helper;

namespace CenazeFonu.MediatR.Commands
{
    public class ChangePasswordCommand : IRequest<ServiceResponse<UserDto>>
    {
        public string UserName { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
