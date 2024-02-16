using MediatR;
using ContentManagement.Data.Dto;
using ContentManagement.Helper;

namespace ContentManagement.MediatR.Commands
{
    public class AddEmailTemplateCommand : IRequest<ServiceResponse<EmailTemplateDto>>
    {
        public string Name { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
