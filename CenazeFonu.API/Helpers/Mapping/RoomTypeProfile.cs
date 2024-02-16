using AutoMapper;
using CenazeFonu.Data.Dto;
using CenazeFonu.Data.Models;
using CenazeFonu.MediatR.Commands;

namespace CenazeFonu.API.Helpers.Mapping
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
