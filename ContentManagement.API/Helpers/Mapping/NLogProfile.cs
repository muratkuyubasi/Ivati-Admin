using AutoMapper;
using ContentManagement.Data;
using ContentManagement.Data.Dto;

namespace ContentManagement.API.Helpers.Mapping
{
    public class NLogProfile : Profile
    {
        public NLogProfile()
        {
            CreateMap<ContentManagement.Data.NLog, NLogDto>().ReverseMap();
        }
    }
}
