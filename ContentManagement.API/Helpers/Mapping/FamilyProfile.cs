using AutoMapper;
using ContentManagement.Data.Dto;
using ContentManagement.Data.Models;
using ContentManagement.MediatR.Commands;

namespace ContentManagement.API.Helpers.Mapping
{
    public class FamilyProfile : Profile
    {

        public FamilyProfile()
        {
            CreateMap<Family,FamilyDTO>().ReverseMap();
            CreateMap<ChangeHeadOfTheFamilyCommand, Family>();
            CreateMap<Family, FamilyInformationDTO>().ReverseMap();
            CreateMap<Family, DeletedFamiliesInformationDTO>().ReverseMap();
            CreateMap<ParentalDivorceCommand, Family>();
            CreateMap<Family, DiedFamilyMembersFamilyInformationDTO>().ReverseMap();
            CreateMap<Family, DebtorFDTO>().ReverseMap();
            CreateMap<Family, FamilySimpleDTO>().ReverseMap();
        }
    }
}
