using AutoMapper;
using CenazeFonu.Data;
using CenazeFonu.Data.Dto;
using CenazeFonu.Data.Models;
using CenazeFonu.MediatR.Commands;

namespace CenazeFonu.API.Helpers.Mapping
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
