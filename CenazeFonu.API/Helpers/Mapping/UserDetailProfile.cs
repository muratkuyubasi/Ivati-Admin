using AutoMapper;
using CenazeFonu.Data.Dto;
using CenazeFonu.Data.Models;

namespace CenazeFonu.API.Helpers.Mapping
{
    public class UserDetailProfile : Profile
    {

        public UserDetailProfile()
        {
            CreateMap<UserDetail, UserDetailDTO>().ReverseMap();
        }
    }
}
