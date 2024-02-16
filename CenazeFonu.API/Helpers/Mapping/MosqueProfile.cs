using AutoMapper;
using CenazeFonu.Data.Dto;
using CenazeFonu.Data.Models;
using CenazeFonu.MediatR.Commands;

namespace CenazeFonu.API.Helpers.Mapping
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
