using ContentManagement.Data.Dto;
using MediatR;
using System.Collections.Generic;
using ContentManagement.Helper;

namespace ContentManagement.MediatR.Queries
{
    public class GetAllAssociationQuery : IRequest<ServiceResponse<List<AssociationDTO>>>
    {
    }
}
