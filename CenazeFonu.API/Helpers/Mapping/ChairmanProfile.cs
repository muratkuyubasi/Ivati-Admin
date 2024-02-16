using AutoMapper;
using CenazeFonu.Data;
using CenazeFonu.Data.Dto;
using CenazeFonu.Data;
using CenazeFonu.Data.Dto;
using CenazeFonu.MediatR.Commands;

namespace CenazeFonu.API.Helpers.Mapping
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
