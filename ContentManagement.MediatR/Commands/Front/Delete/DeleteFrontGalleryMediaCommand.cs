
using ContentManagement.Data.Dto;
using ContentManagement.Helper;
using MediatR;
using System;
using System.Collections.Generic;

namespace PT.MediatR.Commands
{
    public class DeleteFrontGalleryMediaCommand : IRequest<ServiceResponse<FrontGalleryMediaDto>>
    {
        public int Id { get; set; }
    }
}
