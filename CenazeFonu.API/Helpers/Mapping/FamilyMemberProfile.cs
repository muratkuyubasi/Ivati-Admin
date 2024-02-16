using AutoMapper;
using CenazeFonu.Data.Dto;
using CenazeFonu.Data.Models;
using CenazeFonu.MediatR.Commands;

namespace CenazeFonu.API.Helpers.Mapping
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
