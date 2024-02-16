using CenazeFonu.Data.Dto;
using MediatR;
using System.Collections.Generic;
using CenazeFonu.Helper;

namespace CenazeFonu.MediatR.Queries
{
    public class GetAllAgeQuery : IRequest<ServiceResponse<List<AgeDTO>>>
    {
    }
}
