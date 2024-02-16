
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CenazeFonu.Helper;
using CenazeFonu.Data.Dto;

namespace PT.MediatR.Commands
{
    public class DeleteFrontPageRecordCommand : IRequest<ServiceResponse<FrontPageRecordDto>>
    {
        public Guid Code { get; set; }
    }
}
