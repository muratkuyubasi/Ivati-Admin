using ContentManagement.Data.Dto;
using MediatR;
using System;
using ContentManagement.Helper;

namespace ContentManagement.MediatR.Queries
{
    public class GetPageActionQuery : IRequest<ServiceResponse<PageActionDto>>
    {
        public Guid Id { get; set; }
    }
}
