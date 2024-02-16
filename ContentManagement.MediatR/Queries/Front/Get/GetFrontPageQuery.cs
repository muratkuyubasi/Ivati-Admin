
using MediatR;
using System.Collections.Generic;
using System;
using ContentManagement.Helper;
using ContentManagement.Data.Dto;

namespace ContentManagement.MediatR.Queries
{
    public class GetFrontPageQuery : IRequest<ServiceResponse<FrontPageDto>>
    {
        public Guid Code { get; set; }
    }
}
