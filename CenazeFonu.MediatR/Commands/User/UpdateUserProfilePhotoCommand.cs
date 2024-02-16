using MediatR;
using Microsoft.AspNetCore.Http;
using CenazeFonu.Data.Dto;
using CenazeFonu.Helper;

namespace CenazeFonu.MediatR.Commands
{
    public class UpdateUserProfilePhotoCommand : IRequest<ServiceResponse<UserDto>>
    {
        public IFormFileCollection FormFile { get; set; }
        public string RootPath { get; set; }
    }
}
