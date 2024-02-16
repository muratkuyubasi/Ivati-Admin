﻿using MediatR;
using ContentManagement.Data.Dto;
using ContentManagement.Helper;

namespace ContentManagement.MediatR.Commands
{
    public class SocialLoginCommand : IRequest<ServiceResponse<UserAuthDto>>
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Provider { get; set; }
        public string RemoteIp { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}
