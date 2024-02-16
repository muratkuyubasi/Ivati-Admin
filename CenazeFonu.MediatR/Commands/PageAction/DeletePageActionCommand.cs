using CenazeFonu.Data.Dto;
using MediatR;
using System;
using CenazeFonu.Helper;

namespace CenazeFonu.MediatR.Commands
{
    public class DeletePageActionCommand : IRequest<ServiceResponse<PageActionDto>>
    {
        public Guid Id { get; set; }
        public Guid PageId { get; set; }
        public Guid ActionId { get; set; }
    }
}
