
using ContentManagement.Data.Dto;
using ContentManagement.Helper;
using MediatR;
using System.Collections.Generic;

namespace ContentManagement.MediatR.Queries
{
    public class GetAllFrontGalleryMediaQuery : IRequest<ServiceResponse<List<FrontGalleryMediaDto>>>
    {
    }
}
