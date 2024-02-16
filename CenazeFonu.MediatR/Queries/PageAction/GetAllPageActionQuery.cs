using CenazeFonu.Data.Dto;
using MediatR;
using System.Collections.Generic;

namespace CenazeFonu.MediatR.Queries
{
    public class GetAllPageActionQuery : IRequest<List<PageActionDto>>
    {
    }
}
