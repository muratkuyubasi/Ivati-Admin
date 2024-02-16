using AutoMapper;
using ContentManagement.Data.Dto;
using ContentManagement.Data.Models;

namespace ContentManagement.API.Helpers.Mapping
{
    public class GenderProfile : Profile
    {

        public GenderProfile()
        {
            CreateMap<Gender, GenderDTO>().ReverseMap();
        }
    }
}
