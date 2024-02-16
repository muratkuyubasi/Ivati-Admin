using AutoMapper;
using ContentManagement.Data;
using ContentManagement.Data.Dto;
using ContentManagement.Data;
using ContentManagement.Data.Dto;
using ContentManagement.MediatR.Commands;

namespace ContentManagement.API.Helpers.Mapping
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
