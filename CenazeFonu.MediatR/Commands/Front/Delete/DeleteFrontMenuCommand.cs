
using MediatR;
using System;
using System.Collections.Generic;
using CenazeFonu.Helper;
using CenazeFonu.Data.Dto;

namespace PT.MediatR.Commands
{
    public class DeleteFrontMenuCommand : IRequest<ServiceResponse<FrontMenuDto>>
    {
        public Guid Code { get; set; }
    }
}
