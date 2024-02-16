using CenazeFonu.Data.Dto;
using MediatR;
using CenazeFonu.Helper;

namespace CenazeFonu.MediatR.Commands
{
    public class ResetPasswordCommand : IRequest<ServiceResponse<UserDto>>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
