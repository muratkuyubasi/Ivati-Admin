using AutoMapper;
using CenazeFonu.Data.Dto;
using CenazeFonu.Data.Models;

namespace CenazeFonu.API.Helpers.Mapping
{
    public class CitizenshipProfile : Profile
    {

        public CitizenshipProfile()
        {
            CreateMap<Citizenship, CitizenshipDTO>().ReverseMap();
        }
    }
}
