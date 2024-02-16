using MediatR;
using CenazeFonu.Data.Dto;
using CenazeFonu.Helper;

namespace CenazeFonu.MediatR.Commands
{
    public class AddEmailTemplateCommand : IRequest<ServiceResponse<EmailTemplateDto>>
    {
        public string Name { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
