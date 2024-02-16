using MediatR;
using System;
using System.Collections.Generic;
using CenazeFonu.Helper;
using CenazeFonu.Data.Dto;

namespace PT.MediatR.Commands
{
    public class DeleteFrontAnnouncementCommand : IRequest<ServiceResponse<FrontAnnouncementDto>>
    {
        public Guid Code { get; set; }
    }
}
