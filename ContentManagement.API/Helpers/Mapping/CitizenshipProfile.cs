using AutoMapper;
using ContentManagement.Data.Dto;
using ContentManagement.Data.Models;

namespace ContentManagement.API.Helpers.Mapping
{
    public class CitizenshipProfile : Profile
    {

        public CitizenshipProfile()
        {
            CreateMap<Citizenship, CitizenshipDTO>().ReverseMap();
        }
    }
}
