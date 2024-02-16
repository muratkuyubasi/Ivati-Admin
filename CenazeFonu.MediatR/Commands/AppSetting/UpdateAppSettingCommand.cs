using MediatR;
using System;
using CenazeFonu.Data.Dto;
using CenazeFonu.Helper;

namespace CenazeFonu.MediatR.Commands
{
    public class UpdateAppSettingCommand : IRequest<ServiceResponse<AppSettingDto>>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public bool Useditor { get; set; }
    }
}
