using AutoMapper;
using ContentManagement.Data;
using ContentManagement.Data.Dto;
using ContentManagement.Data.Models;
using ContentManagement.MediatR.Commands;

namespace ContentManagement.API.Helpers.Mapping
{
    public class HacProfile : Profile
    {

        public HacProfile()
        {
            CreateMap<HacForm, HacFormDTO>().ReverseMap();
            CreateMap<AddHacFormCommand, HacForm>();


            CreateMap<HacPeriod, HacPeriodDTO>().ReverseMap();
            CreateMap<AddHacPeriodCommand, HacPeriod>();
            CreateMap<UpdateHacPeriodCommand, HacPeriod>();
        }
    }
}
