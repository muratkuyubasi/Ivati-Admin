using AutoMapper;
using CenazeFonu.Data;
using CenazeFonu.Data.Dto;
using CenazeFonu.MediatR.Commands;

namespace CenazeFonu.API.Helpers
{
    public class EmailTemplateProfile : Profile
    {
        public EmailTemplateProfile()
        {
            CreateMap<EmailTemplateDto, EmailTemplate>().ReverseMap();
            CreateMap<AddEmailTemplateCommand, EmailTemplate>();
            CreateMap<UpdateEmailTemplateCommand, EmailTemplate>().ReverseMap();
        }
    }
}
