using MediatR;
using System;
using CenazeFonu.Data.Dto;
using CenazeFonu.Helper;

namespace CenazeFonu.MediatR.Commands
{
    public class DeleteAppSettingCommand : IRequest<ServiceResponse<AppSettingDto>>
    {
        public Guid Id { get; set; }
    }
}
