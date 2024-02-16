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
    public class GetUmreCountByAirportsQueryHandler : IRequestHandler<GetUmreCountByAirportsQuery, Object>
    {
        public GetUmreCountByAirportsQueryHandler(IUmreFormRepository hacRepository, IMapper mapper)
        {
            _repo = hacRepository;
            _mapper = mapper;
        }

        private readonly IUmreFormRepository _repo;
        private readonly IMapper _mapper;

        public async Task<object> Handle(GetUmreCountByAirportsQuery request, CancellationToken cancellationToken)
        {
            var counts = await _repo.All
    .GroupBy(x => x.DepartureAirport.Name)
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
