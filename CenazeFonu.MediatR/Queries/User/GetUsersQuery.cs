using MediatR;
using CenazeFonu.Data.Resources;
using CenazeFonu.Repository;

namespace CenazeFonu.MediatR.Queries
{
    public class GetUsersQuery : IRequest<UserList>
    {
        public UserResource UserResource { get; set; }
    }
}
