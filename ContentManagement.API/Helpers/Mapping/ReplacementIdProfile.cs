using AutoMapper;
using ContentManagement.Data.Dto;
using ContentManagement.Data.Models;

namespace ContentManagement.API.Helpers.Mapping
{
    public class ReplacementIdProfile : Profile
    {
        public ReplacementIdProfile()
        {
            CreateMap<ReplacementId, ReplacementIdDTO>().ReverseMap();
        }
    }
}
