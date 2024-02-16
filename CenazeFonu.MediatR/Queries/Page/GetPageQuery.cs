using CenazeFonu.Data.Dto;
using MediatR;
using System;
using CenazeFonu.Helper;

namespace CenazeFonu.MediatR.Queries
{
    public class GetPageQuery : IRequest<ServiceResponse<PageDto>>
    {
        public Guid Id { get; set; }
    }
}
