using MediatR;
using Microsoft.AspNetCore.Http;
using ContentManagement.Data.Dto;
using ContentManagement.Helper;

namespace ContentManagement.MediatR.Commands
{
    public class UpdateUserProfilePhotoCommand : IRequest<ServiceResponse<UserDto>>
    {
        public IFormFileCollection FormFile { get; set; }
        public string RootPath { get; set; }
    }
}
