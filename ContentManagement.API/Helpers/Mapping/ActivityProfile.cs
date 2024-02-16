using AutoMapper;
using ContentManagement.Data.Dto;
using ContentManagement.Data.Models;
using ContentManagement.MediatR.Commands;

namespace ContentManagement.API.Helpers.Mapping
{
    public class ActivityProfile : Profile
    {
        public ActivityProfile()
        {
            CreateMap<Activity, ActivityDTO>().ReverseMap();
            CreateMap<AddActivityCommand, Activity>();
            CreateMap<UpdateActivityCommand, Activity>();
        }
    }
}
