using MediatR;
using System;
using ContentManagement.Helper;

namespace ContentManagement.MediatR.Commands
{
    public class DeleteEmailTemplateCommand : IRequest<ServiceResponse<bool>>
    {
        public Guid Id { get; set; }
    }
}
