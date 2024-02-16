using AutoMapper;
using ContentManagement.Data.Dto;
using ContentManagement.Data.Models;
using ContentManagement.MediatR.Commands;

namespace ContentManagement.API.Helpers.Mapping
{
    public class AssociationProfile : Profile
    {
        public AssociationProfile()
        {
            CreateMap<Association, AssociationDTO>().ReverseMap();
            CreateMap<AddAssociationCommand, Association>();
            CreateMap<UpdateAssociationCommand, Association>();
        }
    }
}
