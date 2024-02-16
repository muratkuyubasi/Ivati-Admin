using MediatR;
using CenazeFonu.Data.Dto;
using CenazeFonu.Helper;

namespace CenazeFonu.MediatR.Commands
{
    public class AddAppSettingCommand: IRequest<ServiceResponse<AppSettingDto>>
    {
        public string Name { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
