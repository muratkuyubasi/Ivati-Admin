using AutoMapper;
using ContentManagement.Data.Dto;
using ContentManagement.Data.Models;
using ContentManagement.MediatR.Commands;

namespace ContentManagement.API.Helpers.Mapping
{
    public class FamilyMemberProfile : Profile
    {

        public FamilyMemberProfile()
        {
            CreateMap<FamilyMember, FamilyMemberDTO>().ReverseMap();
            CreateMap<AddNewFamilyMemberCommand, FamilyMember>();
            CreateMap<TransferChildrenToAnotherFamilyCommand, FamilyMember>();
            CreateMap<FamilyMember, DiedFamilyMemberDTO>().ReverseMap();
            CreateMap<FamilyMember, FamilyMemberWithFamilyDTO>().ReverseMap();
            CreateMap<FamilyMember, DeletedFamilyMemberWithFamilyDTO>().ReverseMap();
            CreateMap<FamilyMember, FamilyMemberSimpleDTO>().ReverseMap();
        }
    }
}
