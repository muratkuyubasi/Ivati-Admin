using AutoMapper;
using ContentManagement.Data;
using ContentManagement.Data.Dto;
using ContentManagement.MediatR.Commands;
using ContentManagement.MediatR.Commands.User;

namespace ContentManagement.API.Helpers.Mapping
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
