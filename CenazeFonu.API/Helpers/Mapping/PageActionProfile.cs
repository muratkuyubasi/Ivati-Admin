using AutoMapper;
using CenazeFonu.Data;
using CenazeFonu.Data.Dto;
using CenazeFonu.MediatR.Commands;

namespace CenazeFonu.API.Helpers.Mapping
{
    public class PageActionProfile : Profile
    {
        public PageActionProfile()
        {
            CreateMap<PageAction, PageActionDto>().ReverseMap();
            CreateMap<AddPageActionCommand, PageAction>().ReverseMap();
        }
    }
}
