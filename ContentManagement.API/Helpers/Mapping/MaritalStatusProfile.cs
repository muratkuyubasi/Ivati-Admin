using AutoMapper;
using ContentManagement.Data.Dto;
using ContentManagement.Data.Models;

namespace ContentManagement.API.Helpers.Mapping
{
    public class MaritalStatusProfile : Profile
    {
        public MaritalStatusProfile()
        {
            CreateMap<MaritalStatus, MaritalStatusDTO>().ReverseMap();
        }
    }
}
