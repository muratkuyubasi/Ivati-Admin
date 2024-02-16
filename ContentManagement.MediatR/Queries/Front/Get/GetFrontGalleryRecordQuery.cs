
using ContentManagement.Data.Dto;
using ContentManagement.Helper;
using MediatR;
using System.Collections.Generic;

namespace ContentManagement.MediatR.Queries
{
    public class GetFrontGalleryRecordQuery : IRequest<ServiceResponse<FrontGalleryRecordDto>>
    {
        public int Id { get; set; }
    }
}
