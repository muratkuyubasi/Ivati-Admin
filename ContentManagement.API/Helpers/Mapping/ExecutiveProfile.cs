using AutoMapper;
using ContentManagement.Data.Dto;
using ContentManagement.Data.Models;
using ContentManagement.MediatR.Commands;

namespace ContentManagement.API.Helpers.Mapping
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
