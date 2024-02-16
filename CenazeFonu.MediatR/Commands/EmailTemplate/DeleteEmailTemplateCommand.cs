using MediatR;
using System;
using CenazeFonu.Helper;

namespace CenazeFonu.MediatR.Commands
{
    public class DeleteEmailTemplateCommand : IRequest<ServiceResponse<bool>>
    {
        public Guid Id { get; set; }
    }
}
