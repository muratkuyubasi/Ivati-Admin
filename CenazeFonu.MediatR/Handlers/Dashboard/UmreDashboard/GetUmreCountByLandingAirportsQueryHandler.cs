using AutoMapper;
using CenazeFonu.MediatR.Queries;
using CenazeFonu.MediatR.Queries.Dashboard.HacDashoard;
using CenazeFonu.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CenazeFonu.MediatR.Handlers
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
