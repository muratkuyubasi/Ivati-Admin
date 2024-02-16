
using ContentManagement.Data.Dto;
using ContentManagement.Helper;
using MediatR;
using System;
using System.Collections.Generic;

namespace PT.MediatR.Commands
{
    public class DeleteFrontPageCommand : IRequest<ServiceResponse<FrontPageDto>>
    {
        public Guid Code { get; set; }
    }
}
