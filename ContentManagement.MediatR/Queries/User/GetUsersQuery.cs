using MediatR;
using ContentManagement.Data.Resources;
using ContentManagement.Repository;

namespace ContentManagement.MediatR.Queries
{
    public class GetUsersQuery : IRequest<UserList>
    {
        public UserResource UserResource { get; set; }
    }
}
