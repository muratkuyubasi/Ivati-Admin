using MediatR;
using System;
using System.Collections.Generic;
using ContentManagement.Helper;
using ContentManagement.Data.Dto;

namespace PT.MediatR.Commands
{
    public class DeleteFrontAnnouncementCommand : IRequest<ServiceResponse<FrontAnnouncementDto>>
    {
        public Guid Code { get; set; }
    }
}
