using AutoMapper;
using CenazeFonu.Data.Dto;
using CenazeFonu.Data.Models;
using CenazeFonu.MediatR.Commands;

namespace CenazeFonu.API.Helpers.Mapping
{
    public class UserModelProfile : Profile
    {
        public UserModelProfile()
        {
            CreateMap<UserModel, UserModelDTO>().ReverseMap();
            CreateMap<AddUserModelCommand, UserModel>();
        }
    }
}
