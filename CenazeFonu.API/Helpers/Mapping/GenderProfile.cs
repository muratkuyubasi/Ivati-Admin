using AutoMapper;
using CenazeFonu.Data.Dto;
using CenazeFonu.Data.Models;

namespace CenazeFonu.API.Helpers.Mapping
{
    public class GenderProfile : Profile
    {

        public GenderProfile()
        {
            CreateMap<Gender, GenderDTO>().ReverseMap();
        }
    }
}
