
using CenazeFonu.Data.Dto;
using CenazeFonu.Helper;
using MediatR;
using System.Collections.Generic;

namespace CenazeFonu.MediatR.Queries
{
    public class GetAllFrontAnnouncementQuery : IRequest<ServiceResponse<List<FrontAnnouncementDto>>>
    {
        public string LanguageCode { get; set; }
    }
}
