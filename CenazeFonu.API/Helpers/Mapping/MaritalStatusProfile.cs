using AutoMapper;
using CenazeFonu.Data.Dto;
using CenazeFonu.Data.Models;

namespace CenazeFonu.API.Helpers.Mapping
{
    public class MaritalStatusProfile : Profile
    {
        public MaritalStatusProfile()
        {
            CreateMap<MaritalStatus, MaritalStatusDTO>().ReverseMap();
        }
    }
}
