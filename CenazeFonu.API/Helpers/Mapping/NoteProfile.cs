using AutoMapper;
using CenazeFonu.Data.Dto;
using CenazeFonu.Data.Models;
using CenazeFonu.MediatR.Commands;

namespace CenazeFonu.API.Helpers.Mapping
{
    public class NoteProfile : Profile
    {

        public NoteProfile()
        {
            CreateMap<Note, NoteDTO>().ReverseMap();
            CreateMap<AddNoteCommand, Note>();
            CreateMap<UpdateNoteCommand, Note>();
        }
    }
}
