using AutoMapper;
using CenazeFonu.Data.Dto;
using CenazeFonu.Data.Models;
using CenazeFonu.MediatR.Commands;

namespace CenazeFonu.API.Helpers.Mapping
{
    public class ServiceProfile : Profile
    {
        public ServiceProfile()
        {
            CreateMap<Services, ServicesDTO>().ReverseMap();
            CreateMap<AddServicesCommand, Services>();
            CreateMap<UpdateServicesCommand, Services>();
        }
    }
}
