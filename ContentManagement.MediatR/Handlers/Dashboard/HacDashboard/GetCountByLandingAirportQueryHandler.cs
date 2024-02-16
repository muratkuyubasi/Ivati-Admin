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
    public class GetCountByLandingAirportQueryHandler : IRequestHandler<GetCountByLandingAirportQuery, Object>
    {
        private readonly IHacRepository _hacRepository;

        public GetCountByLandingAirportQueryHandler(IHacRepository hacRepository)
        {
            _hacRepository = hacRepository;
        }
        public async Task<object> Handle(GetCountByLandingAirportQuery request, CancellationToken cancellationToken)
        {
            var counts = await _hacRepository.All
    .GroupBy(x => x.LandingAirport.Name)
    .Select(group => new
    {
        Airport = group.Key,
        Count = group.Count()
    })
    .OrderByDescending(x => x.Count)
    .ToListAsync();
            return counts;
        }
    }
}
