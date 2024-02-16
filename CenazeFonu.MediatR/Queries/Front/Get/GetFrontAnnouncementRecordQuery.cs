
using CenazeFonu.Data.Dto;
using CenazeFonu.Helper;
using MediatR;
namespace CenazeFonu.MediatR.Queries
{
    public class GetFrontAnnouncementRecordQuery : IRequest<ServiceResponse<FrontAnnouncementRecordDto>>
    {
        public int Id { get; set; }
    }
}
