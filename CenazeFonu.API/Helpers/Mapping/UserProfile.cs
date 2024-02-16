using AutoMapper;
using CenazeFonu.Data;
using CenazeFonu.Data.Dto;
using CenazeFonu.MediatR.Commands;
using CenazeFonu.MediatR.Commands.User;

namespace CenazeFonu.API.Helpers.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserClaimDto, UserClaim>().ReverseMap();
            CreateMap<UserRoleDto, UserRole>().ReverseMap();
            CreateMap<UserAllowedIPDto, UserAllowedIP>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UserInformationDTO>().ReverseMap();
            CreateMap<AddUserCommand, User>();
            CreateMap<SocialLoginCommand, User>();
            CreateMap<ResetPasswordCommand, UserDto>();
            CreateMap<UpdateFamilyMemberCommand, User>();
            CreateMap<ChangeFamilyActivityStatusCommand, User>();
            CreateMap<ReportFamilyMemberDateOfDeathCommand, User>();
            CreateMap<User, DiedUserInformationDTO>().ReverseMap();
            CreateMap<User, UpdateUserContactDTO>().ReverseMap();
            CreateMap<UpdateUserInformationCommand, User>();
            CreateMap<ResetAllUsersPasswordCommand, User>();
            CreateMap<User, UserSimpleDTO>().ReverseMap();
            CreateMap<ExecutiveUserDTO, User>();
        }
    }
}
