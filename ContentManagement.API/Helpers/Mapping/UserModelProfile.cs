using AutoMapper;
using ContentManagement.Data.Dto;
using ContentManagement.Data.Models;
using ContentManagement.MediatR.Commands;

namespace ContentManagement.API.Helpers.Mapping
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
