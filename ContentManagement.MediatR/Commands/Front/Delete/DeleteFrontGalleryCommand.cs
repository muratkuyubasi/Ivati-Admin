
using MediatR;
using System;
using System.Collections.Generic;
using ContentManagement.Helper;
using ContentManagement.Data.Dto;

namespace PT.MediatR.Commands
{
    public class DeleteFrontGalleryCommand : IRequest<ServiceResponse<FrontGalleryDto>>
    {
        public Guid Code { get; set; }
    }
}
