using MediatR;
using CenazeFonu.Data.Dto;
using CenazeFonu.Helper;

namespace CenazeFonu.MediatR.Queries
{
    public class GetAppSettingByKeyQuery : IRequest<ServiceResponse<AppSettingDto>>
    {
        public string Key { get; set; }
    }
}
