using AutoMapper;
using ContentManagement.Data.Models;
using ContentManagement.MediatR.Commands;

namespace ContentManagement.API.Helpers.Mapping
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
