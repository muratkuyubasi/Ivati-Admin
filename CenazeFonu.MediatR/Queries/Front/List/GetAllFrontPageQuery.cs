
using CenazeFonu.Data.Dto;
using CenazeFonu.Helper;
using MediatR;
using System.Collections.Generic;

namespace CenazeFonu.MediatR.Queries
{
    public class GetAllFrontPageQuery : IRequest<ServiceResponse<List<FrontPageDto>>>
    {
        public string LanguageCode { get; set; }
    }
}
