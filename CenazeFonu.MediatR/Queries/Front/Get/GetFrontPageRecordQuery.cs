
using MediatR;
using System.Collections.Generic;
using System;
using CenazeFonu.Helper;
using CenazeFonu.Data.Dto;

namespace CenazeFonu.MediatR.Queries
{
    public class GetFrontPageRecordQuery : IRequest<ServiceResponse<FrontPageRecordDto>>
    {
        public Guid Code { get; set; }
    }
}
