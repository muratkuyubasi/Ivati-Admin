using AutoMapper;
using ContentManagement.Data.Models;

namespace ContentManagement.API.Helpers.Mapping
{
    public class SpouseProfile : Profile
    {
        public SpouseProfile()
        {
            CreateMap<Spouse, SpouseDTO>().ReverseMap();
        }
    }
}
