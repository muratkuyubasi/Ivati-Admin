using MediatR;
using System.Collections.Generic;
using ContentManagement.Helper;
using ContentManagement.Data.Dto;

namespace ContentManagement.MediatR.Queries
{
    public class GetAllFrontGalleryQuery : IRequest<ServiceResponse<List<FrontGalleryDto>>>
    {
    }
}
