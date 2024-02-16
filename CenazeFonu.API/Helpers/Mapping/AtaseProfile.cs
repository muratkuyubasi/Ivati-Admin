using AutoMapper;
using CenazeFonu.Data;
using CenazeFonu.Data.Dto;
using CenazeFonu.Data;
using CenazeFonu.Data.Dto;
using CenazeFonu.MediatR.Commands;

namespace CenazeFonu.API.Helpers.Mapping
{
    public class AtaseProfile : Profile
    {
        public AtaseProfile()
        {
            CreateMap<AtaseModel, AtaseModelDTO>().ReverseMap();
            CreateMap<AddAtaseCommand, AtaseModel>();
            CreateMap<UpdateAtaseCommand, AtaseModel>();
        }
    }
}
