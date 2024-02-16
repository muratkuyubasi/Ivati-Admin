using AutoMapper;
using ContentManagement.Data.Dto;
using ContentManagement.Data.Models;
using ContentManagement.MediatR.Commands;

namespace ContentManagement.API.Helpers.Mapping
{
    public class AirportProfile : Profile
    {

        public AirportProfile()
        {
            CreateMap<Airport, AirportDTO>().ReverseMap();
            CreateMap<AddAirportCommand, Airport>();
            CreateMap<UpdateAirportCommand, Airport>();
        }
    }
}
