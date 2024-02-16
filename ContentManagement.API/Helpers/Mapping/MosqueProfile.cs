using AutoMapper;
using ContentManagement.Data.Dto;
using ContentManagement.Data.Models;
using ContentManagement.MediatR.Commands;

namespace ContentManagement.API.Helpers.Mapping
{
    public class MosqueProfile : Profile
    {
        public MosqueProfile()
        {
            CreateMap<Mosque, MosqueDTO>().ReverseMap();
            CreateMap<AddMosqueCommand, Mosque>();
            CreateMap<UpdateMosqueCommand, Mosque>();
        }
    }
}
