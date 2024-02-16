using AutoMapper;
using ContentManagement.Data.Dto;
using ContentManagement.Data.Models;
using ContentManagement.MediatR.Commands;

namespace ContentManagement.API.Helpers.Mapping
{
    public class RoomTypeProfile : Profile
    {

        public RoomTypeProfile()
        {
            CreateMap<RoomType, RoomTypeDTO>().ReverseMap();
            CreateMap<AddRoomTypeCommand, RoomType>();
            CreateMap<UpdateRoomTypeCommand, RoomType>();
        }
    }
}
