using AutoMapper;
using CenazeFonu.Data;
using CenazeFonu.Data.Dto;

namespace CenazeFonu.API.Helpers.Mapping
{
    public class NLogProfile : Profile
    {
        public NLogProfile()
        {
            CreateMap<CenazeFonu.Data.NLog, NLogDto>().ReverseMap();
        }
    }
}
