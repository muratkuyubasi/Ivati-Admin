using MediatR;
using System.Collections.Generic;
using ContentManagement.Data.Dto;

namespace ContentManagement.MediatR.Queries
{
    public class GetEmailSMTPSettingsQuery : IRequest<List<EmailSMTPSettingDto>>
    {
    }
}
