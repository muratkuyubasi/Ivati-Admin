using CenazeFonu.Data.Dto;
using CenazeFonu.Helper;
using MediatR;
using System;
namespace CenazeFonu.MediatR.Queries
{
    public class GetFrontAnnouncementQuery : IRequest<ServiceResponse<FrontAnnouncementDto>>
    {
        public Guid Code { get; set; }
    }
}
