using AutoMapper;
using ContentManagement.Data;
using ContentManagement.Data.Dto;
using ContentManagement.Data.Models;
using ContentManagement.MediatR.Commands;

namespace ContentManagement.API.Helpers.Mapping
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
