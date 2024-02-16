using AutoMapper;
using ContentManagement.Data;
using ContentManagement.Data.Dto;
using ContentManagement.Data.Models;
using ContentManagement.MediatR.Commands;

namespace ContentManagement.API.Helpers.Mapping
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
