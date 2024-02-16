using AutoMapper;
using ContentManagement.Data.Dto;
using ContentManagement.Data.Models;
using ContentManagement.MediatR.Commands;

namespace ContentManagement.API.Helpers.Mapping
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
