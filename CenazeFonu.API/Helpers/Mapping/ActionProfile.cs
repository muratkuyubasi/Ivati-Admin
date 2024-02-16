using AutoMapper;
using CenazeFonu.Data;
using CenazeFonu.Data.Dto;
using CenazeFonu.MediatR.Commands;
using Action = CenazeFonu.Data.Action;

namespace CenazeFonu.API.Helpers.Mapping
{
    public class ActionProfile : Profile
    {
        public ActionProfile()
        {
            CreateMap<Action, ActionDto>().ReverseMap();
            CreateMap<AddActionCommand, Action>();
            CreateMap<UpdateActionCommand, Action>();
        }
    }
}
