using AutoMapper;
using CenazeFonu.Data.Dto;
using CenazeFonu.Data.Models;
using CenazeFonu.MediatR.Commands;

namespace CenazeFonu.API.Helpers.Mapping
{
    public class ExecutiveProfile : Profile
    {
        public ExecutiveProfile()
        {
            CreateMap<Executive, ExecutiveDTO>().ReverseMap();
            CreateMap<AddExecutiveCommand, Executive>();
            CreateMap<UpdateExecutiveCommand, Executive>();
        }
    }
}
