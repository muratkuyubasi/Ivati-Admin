using AutoMapper;
using ContentManagement.Data;
using ContentManagement.Data.Dto;
using ContentManagement.MediatR.Commands;

namespace ContentManagement.API.Helpers.Mapping
{
    public class TranslationProfile : Profile
    {
        public TranslationProfile()
        {
            CreateMap<Translation, TranslationDTO>().ReverseMap();
            CreateMap<AddTranslationCommand, Translation>();
            CreateMap<UpdateTranslationCommand, Translation>();
        }
    }
}
