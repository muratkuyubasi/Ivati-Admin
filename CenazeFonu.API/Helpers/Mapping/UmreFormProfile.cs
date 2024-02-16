using AutoMapper;
using CenazeFonu.Data;
using CenazeFonu.Data.Dto;
using CenazeFonu.Data.Models;
using CenazeFonu.MediatR.Commands;

namespace CenazeFonu.API.Helpers.Mapping
{
    public class UmreFormProfile : Profile
    {
        public UmreFormProfile()
        {
            CreateMap<UmreForm, UmreFormDTO>().ReverseMap();
            CreateMap<AddUmreFormCommand, UmreForm>();

            CreateMap<UmrePeriod, UmrePeriodDTO>().ReverseMap();
            CreateMap<AddUmrePeriodCommand, UmrePeriod>();
            CreateMap<UpdateUmrePeriodCommand, UmrePeriod>();
        }
    }
}
