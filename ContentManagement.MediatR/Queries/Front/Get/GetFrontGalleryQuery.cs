using ContentManagement.Data.Dto;
using ContentManagement.Helper;
using MediatR;
using System;

namespace ContentManagement.MediatR.Queries
{
    public class GetFrontGalleryQuery : IRequest<ServiceResponse<FrontGalleryDto>>
    {
        public Guid Code { get; set; }
    }
}
