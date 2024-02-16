using CenazeFonu.Data.Dto;
using MediatR;
using System;
using CenazeFonu.Helper;

namespace CenazeFonu.MediatR.Commands
{
    public class AddActionCommand: IRequest<ServiceResponse<ActionDto>>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
