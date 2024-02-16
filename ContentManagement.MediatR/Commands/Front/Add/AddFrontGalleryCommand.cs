using MediatR;
using System;
using System.Collections.Generic;
using ContentManagement.Helper;
using ContentManagement.Data.Dto;

namespace PT.MediatR.Commands
{
    public class AddFrontGalleryCommand : IRequest<ServiceResponse<FrontGalleryDto>>
    {
        public short Order { get; set; }
        public bool IsActive { get; set; }
    }
}
