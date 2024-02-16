using AutoMapper;
using CenazeFonu.Data.Dto;
using CenazeFonu.Data.Models;
using CenazeFonu.MediatR.Commands;

namespace CenazeFonu.API.Helpers.Mapping
{
    public class ClergyProfile : Profile
    {
        public ClergyProfile()
        {
            CreateMap<Clergy, ClergyDTO>().ReverseMap();
            CreateMap<AddClergyCommand, Clergy>();
            CreateMap<UpdateClergyCommand, Clergy>();
        }
    }
}
