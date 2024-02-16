using AutoMapper;
using ContentManagement.Data.Dto;
using ContentManagement.Data.Models;

namespace ContentManagement.API.Helpers.Mapping
{
    public class UserDetailProfile : Profile
    {

        public UserDetailProfile()
        {
            CreateMap<UserDetail, UserDetailDTO>().ReverseMap();
        }
    }
}
