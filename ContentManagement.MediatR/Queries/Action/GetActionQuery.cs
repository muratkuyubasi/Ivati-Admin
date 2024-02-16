using ContentManagement.Data.Dto;
using MediatR;
using System;
using ContentManagement.Helper;

namespace ContentManagement.MediatR.Queries
{
    public class GetActionQuery : IRequest<ServiceResponse<ActionDto>>
    {
        public Guid Id { get; set; }
    }
}
