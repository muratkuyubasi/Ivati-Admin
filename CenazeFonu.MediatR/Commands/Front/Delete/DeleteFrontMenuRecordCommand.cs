
using CenazeFonu.Data.Dto;
using CenazeFonu.Helper;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PT.MediatR.Commands
{
    public class DeleteFrontMenuRecordCommand : IRequest<ServiceResponse<FrontMenuRecordDto>>
    {
        public int Id { get; set; }
    }
}
