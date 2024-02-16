using ContentManagement.Data.Dto;
using ContentManagement.Helper;
using MediatR;
using System.Collections.Generic;

namespace ContentManagement.MediatR.Queries
{
    public class GetFrontMenuRecordQuery : IRequest<ServiceResponse<FrontMenuRecordDto>>
    {
        public int Id { get; set; }
    }
}
