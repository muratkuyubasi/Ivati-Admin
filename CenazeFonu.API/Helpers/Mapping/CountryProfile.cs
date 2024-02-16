using CenazeFonu.Data.Dto;
using CenazeFonu.Data;
using CenazeFonu.MediatR.Commands;
using AutoMapper;

namespace CenazeFonu.API.Helpers.Mapping
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
