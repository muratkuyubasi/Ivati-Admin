
using ContentManagement.Data.Dto;
using ContentManagement.Helper;
using MediatR;
using System.Collections.Generic;

namespace ContentManagement.MediatR.Queries
{
    public class GetAllFrontPageQuery : IRequest<ServiceResponse<List<FrontPageDto>>>
    {
        public string LanguageCode { get; set; }
    }
}
