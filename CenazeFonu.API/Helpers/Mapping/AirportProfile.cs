using AutoMapper;
using CenazeFonu.Data.Dto;
using CenazeFonu.Data.Models;
using CenazeFonu.MediatR.Commands;

namespace CenazeFonu.API.Helpers.Mapping
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
