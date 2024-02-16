using AutoMapper;
using ContentManagement.Data.Dto;
using ContentManagement.Data.Models;
using ContentManagement.MediatR.Commands;

namespace ContentManagement.API.Helpers.Mapping
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
