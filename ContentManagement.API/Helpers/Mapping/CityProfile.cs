using AutoMapper;
using ContentManagement.Data;
using ContentManagement.Data.Dto;
using ContentManagement.MediatR.Commands;

namespace ContentManagement.API.Helpers.Mapping
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
