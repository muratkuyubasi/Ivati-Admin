using MediatR;
using System;
using ContentManagement.Data.Dto;
using ContentManagement.Helper;

namespace ContentManagement.MediatR.Commands
{
    public class DeleteEmailSMTPSettingCommand : IRequest<ServiceResponse<EmailSMTPSettingDto>>
    {
        public Guid Id { get; set; }
    }
}
