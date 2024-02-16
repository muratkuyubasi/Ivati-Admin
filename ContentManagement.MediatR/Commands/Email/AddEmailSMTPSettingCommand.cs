﻿using MediatR;
using ContentManagement.Data.Dto;
using ContentManagement.Helper;

namespace ContentManagement.MediatR.Commands
{
    public class AddEmailSMTPSettingCommand : IRequest<ServiceResponse<EmailSMTPSettingDto>>
    {
        public string Host { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsEnableSSL { get; set; }
        public int Port { get; set; }
        public bool IsDefault { get; set; }
    }
}
