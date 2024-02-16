using AutoMapper;
using CenazeFonu.Data.Models;

namespace CenazeFonu.API.Helpers.Mapping
{
    public class SpouseProfile : Profile
    {
        public SpouseProfile()
        {
            CreateMap<Spouse, SpouseDTO>().ReverseMap();
        }
    }
}
