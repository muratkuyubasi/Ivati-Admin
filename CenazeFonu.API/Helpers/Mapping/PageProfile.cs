using AutoMapper;
using CenazeFonu.Data;
using CenazeFonu.Data.Dto;
using CenazeFonu.MediatR.Commands;

namespace CenazeFonu.API.Helpers.Mapping
{
    public class PageProfile : Profile
    {
        public PageProfile()
        {
            CreateMap<Page, PageDto>().ReverseMap();
            CreateMap<AddPageCommand, Page>();
            CreateMap<UpdatePageCommand, Page>();
        }
    }
}
