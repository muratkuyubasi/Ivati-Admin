using CenazeFonu.Data.Dto;
using MediatR;
using System;
using CenazeFonu.Helper;

namespace CenazeFonu.MediatR.Queries
{
    public class GetPageActionQuery : IRequest<ServiceResponse<PageActionDto>>
    {
        public Guid Id { get; set; }
    }
}
