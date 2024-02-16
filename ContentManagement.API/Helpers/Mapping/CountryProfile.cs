using ContentManagement.Data.Dto;
using ContentManagement.Data;
using ContentManagement.MediatR.Commands;
using AutoMapper;

namespace ContentManagement.API.Helpers.Mapping
{
    public class CountryProfile : Profile
    {
        public CountryProfile()
        {
            CreateMap<Country, CountryDTO>().ReverseMap();
            CreateMap<AddCountryCommand, Country>();
            CreateMap<UpdateCountryCommand, Country>();
        }
    }
}
