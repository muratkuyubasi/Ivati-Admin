using AutoMapper;
using CenazeFonu.Data.Dto;
using CenazeFonu.Data.Models;
using CenazeFonu.MediatR.Commands;

namespace CenazeFonu.API.Helpers.Mapping
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
