using CenazeFonu.Data.Dto;
using CenazeFonu.Helper;
using MediatR;
using System;

namespace CenazeFonu.MediatR.Queries
{
    public class GetFrontGalleryQuery : IRequest<ServiceResponse<FrontGalleryDto>>
    {
        public Guid Code { get; set; }
    }
}
