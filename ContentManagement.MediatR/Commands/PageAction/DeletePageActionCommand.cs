using ContentManagement.Data.Dto;
using MediatR;
using System;
using ContentManagement.Helper;

namespace ContentManagement.MediatR.Commands
{
    public class DeletePageActionCommand : IRequest<ServiceResponse<PageActionDto>>
    {
        public Guid Id { get; set; }
        public Guid PageId { get; set; }
        public Guid ActionId { get; set; }
    }
}
