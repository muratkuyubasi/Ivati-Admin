using CenazeFonu.Data.Dto;
using MediatR;
using System;
using CenazeFonu.Helper;

namespace CenazeFonu.MediatR.Commands
{
    public class DeletePageCommand : IRequest<ServiceResponse<PageDto>>
    {
        public Guid Id { get; set; }
    }
}
