using ContentManagement.Data.Dto;
using MediatR;
using System;
using ContentManagement.Helper;
using System.Collections.Generic;

namespace PT.MediatR.Commands
{
    public class UpdateFrontGalleryCommand : IRequest<ServiceResponse<FrontGalleryDto>>
    {
        public int Id { get; set; }
        public Guid Code { get; set; }
        public short Order { get; set; }
        public bool IsActive { get; set; }
    }
}
