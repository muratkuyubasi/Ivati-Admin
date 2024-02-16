using MediatR;

namespace ContentManagement.MediatR.Queries
{
    public class GetInactiveUserCountQuery : IRequest<int>
    {
    }
}
