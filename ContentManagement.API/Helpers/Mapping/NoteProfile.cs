using AutoMapper;
using ContentManagement.Data.Dto;
using ContentManagement.Data.Models;
using ContentManagement.MediatR.Commands;

namespace ContentManagement.API.Helpers.Mapping
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
