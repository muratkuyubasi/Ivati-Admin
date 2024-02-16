
using ContentManagement.Data.Dto;
using ContentManagement.Helper;
using MediatR;
using System.Collections.Generic;

namespace ContentManagement.MediatR.Queries
{
    public class GetAllFrontAnnouncementQuery : IRequest<ServiceResponse<List<FrontAnnouncementDto>>>
    {
        public string LanguageCode { get; set; }
    }
}
