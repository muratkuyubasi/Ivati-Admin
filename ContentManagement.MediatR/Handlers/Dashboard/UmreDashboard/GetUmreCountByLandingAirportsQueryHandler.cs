using AutoMapper;
using ContentManagement.MediatR.Queries;
using ContentManagement.MediatR.Queries.Dashboard.HacDashoard;
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
    public class GetUmreCountByLandingAirportsQueryHandler : IRequestHandler<GetUmreCountByLandingAirportQuery, Object>
    {
        private readonly IUmreFormRepository _repo;
        public GetUmreCountByLandingAirportsQueryHandler(IUmreFormRepository umreForm)
        {
            _repo = umreForm;
        }

        public async Task<object> Handle(GetUmreCountByLandingAirportQuery request, CancellationToken cancellationToken)
        {
            var counts = await _repo.All
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
