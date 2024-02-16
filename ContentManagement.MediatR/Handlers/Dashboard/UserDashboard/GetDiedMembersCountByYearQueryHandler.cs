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
    public class GetDiedMembersCountByYearQueryHandler : IRequestHandler<GetDiedMembersCountByYearQuery, Object>
    {
        private readonly IUserRepository _userRepository;

        public GetDiedMembersCountByYearQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<object> Handle(GetDiedMembersCountByYearQuery request, CancellationToken cancellationToken)
        {
            var count = await _userRepository.All.Where(x=>x.DateOfDeath != null && x.IsDead == true).GroupBy(x => x.DateOfDeath.Value.Year).Select(x => new
            {
                Year = x.Key,
                Count = x.Count()
            }).OrderByDescending(x=>x.Count).ToListAsync();
            return count;
        }
    }
}
