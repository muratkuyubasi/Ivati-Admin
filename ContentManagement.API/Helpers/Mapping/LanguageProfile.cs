using AutoMapper;
using ContentManagement.Data;
using ContentManagement.Data.Dto;
using ContentManagement.MediatR.Commands;

namespace ContentManagement.API.Helpers.Mapping
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
