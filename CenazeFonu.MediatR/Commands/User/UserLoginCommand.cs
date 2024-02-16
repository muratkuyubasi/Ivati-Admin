using CenazeFonu.Data.Dto;
using MediatR;
using CenazeFonu.Helper;

namespace CenazeFonu.MediatR.Commands
{
    public class UserLoginCommand : IRequest<ServiceResponse<UserAuthDto>>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string RemoteIp { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}
