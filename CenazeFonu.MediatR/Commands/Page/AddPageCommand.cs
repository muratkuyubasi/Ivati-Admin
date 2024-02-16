using CenazeFonu.Data.Dto;
using MediatR;
using System;
using CenazeFonu.Helper;

namespace CenazeFonu.MediatR.Commands
{
    public class AddPageCommand : IRequest<ServiceResponse<PageDto>>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
    }
}
