using CenazeFonu.Data.Dto;
using MediatR;
using System;
using CenazeFonu.Helper;

namespace CenazeFonu.MediatR.Commands
{
    public class AddPageActionCommand: IRequest<ServiceResponse<PageActionDto>>
    {
        public Guid PageId { get; set; }
        public Guid ActionId { get; set; }
        public bool Flag { get; set; }
    }
}
