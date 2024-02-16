using AutoMapper;
using CenazeFonu.Data;
using CenazeFonu.Data.Dto;
using CenazeFonu.MediatR.Commands;

namespace CenazeFonu.API.Helpers.Mapping
{
    public class RoleProfile: Profile
    {
        public RoleProfile()
        {
            CreateMap<RoleClaim, RoleClaimDto>().ReverseMap();
            CreateMap<Role, RoleDto>().ReverseMap();
            CreateMap<AddRoleCommand, Role>();
            CreateMap<UpdateRoleCommand, Role>();
        }
    }
}
