using CenazeFonu.Data.Dto;
using MediatR;
using System.Collections.Generic;
using CenazeFonu.Helper;

namespace CenazeFonu.MediatR.Queries
{
    public class GetAllActionQuery : IRequest<ServiceResponse<List<ActionDto>>>
    {
    }
}
