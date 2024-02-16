
using MediatR;
using System.Collections.Generic;
using System;
using CenazeFonu.Helper;
using CenazeFonu.Data.Dto;

namespace CenazeFonu.MediatR.Queries
{
    public class GetFrontPageQuery : IRequest<ServiceResponse<FrontPageDto>>
    {
        public Guid Code { get; set; }
    }
}
