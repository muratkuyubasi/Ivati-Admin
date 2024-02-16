using AutoMapper;
using CenazeFonu.Data;
using CenazeFonu.Data.Dto;
using CenazeFonu.MediatR.Commands;

namespace CenazeFonu.API.Helpers.Mapping
{
    public class LanguageProfile : Profile
    {
        public LanguageProfile()
        {
            CreateMap<Language, LanguageDTO>().ReverseMap();
            CreateMap<AddLanguageCommand, Language>();
            CreateMap<UpdateLanguageCommand, Language>();
        }
    }
}
