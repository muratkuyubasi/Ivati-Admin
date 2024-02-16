using MediatR;
using System.Collections.Generic;
using System;
using ContentManagement.Helper;
using ContentManagement.Data.Dto;

namespace ContentManagement.MediatR.Queries
{
    public class GetFrontMenuQuery : IRequest<ServiceResponse<FrontMenuDto>>
    {
        public Guid Code { get; set; }
    }
}
