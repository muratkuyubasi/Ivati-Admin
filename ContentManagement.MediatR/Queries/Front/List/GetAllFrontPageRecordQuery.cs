
using ContentManagement.Data.Dto;
using ContentManagement.Helper;
using MediatR;
using System.Collections.Generic;

namespace ContentManagement.MediatR.Queries
{
    public class GetAllFrontPageRecordQuery : IRequest<ServiceResponse<List<FrontPageRecordDto>>>
    {
    }
}
