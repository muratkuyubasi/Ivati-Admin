
using CenazeFonu.Data.Dto;
using CenazeFonu.Helper;
using MediatR;
using System.Collections.Generic;

namespace CenazeFonu.MediatR.Queries
{
    public class GetAllFrontAnnouncementRecordQuery : IRequest<ServiceResponse<List<FrontAnnouncementRecordDto>>>
    {
    }
}
