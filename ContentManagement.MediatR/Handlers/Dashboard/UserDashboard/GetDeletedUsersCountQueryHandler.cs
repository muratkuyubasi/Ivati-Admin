using ContentManagement.MediatR.Queries;
using ContentManagement.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ContentManagement.MediatR.Handlers
{
    public class GetDeletedUsersCountQueryHandler : IRequestHandler<GetDeletedUsersCountQuery, int>
    {
        private readonly IUserRepository _userRepository;

        public GetDeletedUsersCountQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public  Task<int> Handle(GetDeletedUsersCountQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_userRepository.All.Where(c => c.IsDeleted).Count());
        }
    }
}
