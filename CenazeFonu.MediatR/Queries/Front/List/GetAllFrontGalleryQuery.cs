using MediatR;
using System.Collections.Generic;
using CenazeFonu.Helper;
using CenazeFonu.Data.Dto;

namespace CenazeFonu.MediatR.Queries
{
    public class GetAllFrontGalleryQuery : IRequest<ServiceResponse<List<FrontGalleryDto>>>
    {
    }
}
