using AutoMapper;
using ContentManagement.Data.Dto;
using ContentManagement.Data.Models;
using ContentManagement.MediatR.Commands;

namespace ContentManagement.API.Helpers.Mapping
{
    public class FoundationPublicationProfile : Profile
    {
        public FoundationPublicationProfile()
        {
            CreateMap<FoundationPublication, FoundationPublicationDTO>().ReverseMap();
            CreateMap<AddFoundationPublicationCommand, FoundationPublication>();
            CreateMap<UpdateFoundationPublicationCommand, FoundationPublication>();
        }
    }
}
