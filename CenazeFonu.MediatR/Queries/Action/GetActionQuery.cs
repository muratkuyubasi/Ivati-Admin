using CenazeFonu.Data.Dto;
using MediatR;
using System;
using CenazeFonu.Helper;

namespace CenazeFonu.MediatR.Queries
{
    public class GetActionQuery : IRequest<ServiceResponse<ActionDto>>
    {
        public Guid Id { get; set; }
    }
}
