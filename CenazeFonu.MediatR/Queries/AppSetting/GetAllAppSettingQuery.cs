using MediatR;
using System.Collections.Generic;
using CenazeFonu.Data.Dto;
using CenazeFonu.Helper;

namespace CenazeFonu.MediatR.Queries
{
    public class GetAllAppSettingQuery : IRequest<ServiceResponse<List<AppSettingDto>>>
    {
    }
}
