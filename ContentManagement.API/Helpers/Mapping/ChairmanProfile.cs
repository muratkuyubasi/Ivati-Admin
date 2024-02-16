using AutoMapper;
using ContentManagement.Data;
using ContentManagement.Data.Dto;
using ContentManagement.Data;
using ContentManagement.Data.Dto;
using ContentManagement.MediatR.Commands;

namespace ContentManagement.API.Helpers.Mapping
{
    public class ChairmanProfile : Profile
    {
        public ChairmanProfile()
        {
            CreateMap<Chairman, ChairmanDTO>().ReverseMap();
            CreateMap<AddChairmanCommand, Chairman>();
            CreateMap<UpdateChairmanCommand, Chairman>();
        }
    }
}
