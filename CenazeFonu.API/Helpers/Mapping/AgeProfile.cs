using AutoMapper;
using CenazeFonu.Data.Dto;
using CenazeFonu.Data.Models;
using CenazeFonu.MediatR.Commands;

namespace CenazeFonu.API.Helpers.Mapping
{
    public class AgeProfile : Profile
    {

        public AgeProfile()
        {
            CreateMap<Age,AgeDTO>().ReverseMap();
            CreateMap<AddAgeCommand, Age>();
            CreateMap<UpdateAgeCommand, Age>();
        }
    }
}
