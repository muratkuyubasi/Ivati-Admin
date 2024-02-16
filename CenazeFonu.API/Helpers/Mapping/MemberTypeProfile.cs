using AutoMapper;
using CenazeFonu.Data.Models;
using CenazeFonu.MediatR.Commands;

namespace CenazeFonu.API.Helpers.Mapping
{
    public class MemberTypeProfile : Profile
    {
        public MemberTypeProfile()
        {
            CreateMap<MemberType, MemberTypeDTO>().ReverseMap();
            CreateMap<AddMemberTypeCommand, MemberType>().ReverseMap();
            CreateMap<UpdateMemberTypeCommand, MemberType>().ReverseMap();
        }
    }
}
