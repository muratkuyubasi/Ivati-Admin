using AutoMapper;
using CenazeFonu.Data.Dto;
using CenazeFonu.Data.Models;
using CenazeFonu.MediatR.Commands;

namespace CenazeFonu.API.Helpers.Mapping
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
