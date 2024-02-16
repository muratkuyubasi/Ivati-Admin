using MediatR;
using ContentManagement.Data.Dto;
using ContentManagement.Helper;

namespace ContentManagement.MediatR.Queries
{
    public class GetAppSettingByKeyQuery : IRequest<ServiceResponse<AppSettingDto>>
    {
        public string Key { get; set; }
    }
}
