
using MediatR;
using System;
using System.Collections.Generic;
using ContentManagement.Helper;
using ContentManagement.Data.Dto;

namespace PT.MediatR.Commands
{
    public class DeleteFrontMenuCommand : IRequest<ServiceResponse<FrontMenuDto>>
    {
        public Guid Code { get; set; }
    }
}
