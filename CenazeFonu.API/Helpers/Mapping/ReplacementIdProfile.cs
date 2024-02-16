using AutoMapper;
using CenazeFonu.Data.Dto;
using CenazeFonu.Data.Models;

namespace CenazeFonu.API.Helpers.Mapping
{
    public class ReplacementIdProfile : Profile
    {
        public ReplacementIdProfile()
        {
            CreateMap<ReplacementId, ReplacementIdDTO>().ReverseMap();
        }
    }
}
