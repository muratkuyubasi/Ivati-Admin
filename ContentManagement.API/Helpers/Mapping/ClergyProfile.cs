using AutoMapper;
using ContentManagement.Data.Dto;
using ContentManagement.Data.Models;
using ContentManagement.MediatR.Commands;

namespace ContentManagement.API.Helpers.Mapping
{
    public class ClergyProfile : Profile
    {
        public ClergyProfile()
        {
            CreateMap<Clergy, ClergyDTO>().ReverseMap();
            CreateMap<AddClergyCommand, Clergy>();
            CreateMap<UpdateClergyCommand, Clergy>();
        }
    }
}
