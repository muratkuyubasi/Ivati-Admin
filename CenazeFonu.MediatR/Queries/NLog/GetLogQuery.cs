using MediatR;
using System;
using CenazeFonu.Data.Dto;
using CenazeFonu.Helper;

namespace CenazeFonu.MediatR.Queries
{
    public class GetLogQuery : IRequest<ServiceResponse<NLogDto>>
    {
        public Guid Id { get; set; }
    }
}
