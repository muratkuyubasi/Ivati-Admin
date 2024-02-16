using MediatR;
using System;
using CenazeFonu.Data.Dto;
using CenazeFonu.Helper;

namespace CenazeFonu.MediatR.Queries
{
    public class GetEmailTemplateQuery : IRequest<ServiceResponse<EmailTemplateDto>>
    {
        public Guid Id { get; set; }
    }
}
