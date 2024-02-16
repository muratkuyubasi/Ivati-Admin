using AutoMapper;
using CenazeFonu.Data;
using CenazeFonu.Data.Dto;
using CenazeFonu.Data.Models;
using CenazeFonu.MediatR.Commands;

namespace CenazeFonu.API.Helpers.Mapping
{
    public class FamilyNoteProfile : Profile
    {
        public FamilyNoteProfile()
        {
            CreateMap<FamilyNote, FamilyNoteDTO>().ReverseMap();
            CreateMap<AddFamilyNoteCommand, FamilyNote>();
            CreateMap<UpdateFamilyNoteCommand, FamilyNote>();
        }
    }
}
