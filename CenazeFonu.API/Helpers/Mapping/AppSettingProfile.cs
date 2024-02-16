using AutoMapper;
using CenazeFonu.Data;
using CenazeFonu.Data.Dto;
using CenazeFonu.MediatR.Commands;

namespace CenazeFonu.API.Helpers.Mapping
{
    public class AppSettingProfile : Profile
    {
        public AppSettingProfile()
        {
            CreateMap<AppSettingDto, AppSetting>().ReverseMap();
            CreateMap<AddAppSettingCommand, AppSetting>();
            CreateMap<UpdateAppSettingCommand, AppSetting>().ReverseMap();

        }
    }
}
