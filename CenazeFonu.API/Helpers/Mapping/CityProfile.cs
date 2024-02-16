using AutoMapper;
using CenazeFonu.Data;
using CenazeFonu.Data.Dto;
using CenazeFonu.MediatR.Commands;

namespace CenazeFonu.API.Helpers.Mapping
{
    public class CityProfile : Profile
    {
        public CityProfile()
        {
            CreateMap<City, CityDTO>().ReverseMap();
            CreateMap<AddCityCommand, City>();
            CreateMap<UpdateCityCommand, City>();

        }
    }
}
