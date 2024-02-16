using AutoMapper;
using CenazeFonu.Data.Dto;
using CenazeFonu.Data.Models;
using CenazeFonu.MediatR.Commands;

namespace CenazeFonu.API.Helpers.Mapping
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
