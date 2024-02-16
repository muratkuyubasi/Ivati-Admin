using ContentManagement.Data.Dto;
using MediatR;
using System.Collections.Generic;

namespace ContentManagement.MediatR.Queries
{
    public class GetAllPageActionQuery : IRequest<List<PageActionDto>>
    {
    }
}
