
using ContentManagement.Data.Dto;
using ContentManagement.Helper;
using MediatR;
namespace ContentManagement.MediatR.Queries
{
    public class GetFrontAnnouncementRecordQuery : IRequest<ServiceResponse<FrontAnnouncementRecordDto>>
    {
        public int Id { get; set; }
    }
}
