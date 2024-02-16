using AutoMapper;
using CenazeFonu.Data;
using CenazeFonu.Data.Dto;
using CenazeFonu.MediatR.Commands;

namespace CenazeFonu.API.Helpers.Mapping
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
