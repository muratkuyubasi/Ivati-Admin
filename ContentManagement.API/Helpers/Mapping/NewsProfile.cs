using AutoMapper;
using ContentManagement.Data.Dto;
using ContentManagement.Data.Models;
using ContentManagement.DataDto;
using ContentManagement.MediatR.Commands;

namespace ContentManagement.API.Helpers.Mapping
{
    public class NewsProfile : Profile
    {
        public NewsProfile()
        {
            CreateMap<News, NewsDTO>().ReverseMap();
            CreateMap<AddNewsCommand, News>();
            CreateMap<UpdateNewsCommand, News>();
        }
    }
}
