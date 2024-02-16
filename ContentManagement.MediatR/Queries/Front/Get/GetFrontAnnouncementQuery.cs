using ContentManagement.Data.Dto;
using ContentManagement.Helper;
using MediatR;
using System;
namespace ContentManagement.MediatR.Queries
{
    public class GetFrontAnnouncementQuery : IRequest<ServiceResponse<FrontAnnouncementDto>>
    {
        public Guid Code { get; set; }
    }
}
