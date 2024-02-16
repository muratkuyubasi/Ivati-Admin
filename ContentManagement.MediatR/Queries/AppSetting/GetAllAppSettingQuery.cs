using MediatR;
using System.Collections.Generic;
using ContentManagement.Data.Dto;
using ContentManagement.Helper;

namespace ContentManagement.MediatR.Queries
{
    public class GetAllAppSettingQuery : IRequest<ServiceResponse<List<AppSettingDto>>>
    {
    }
}
