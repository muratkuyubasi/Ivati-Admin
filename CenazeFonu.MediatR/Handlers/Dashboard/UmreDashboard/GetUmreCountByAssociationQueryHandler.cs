using AutoMapper;
using CenazeFonu.MediatR.Queries;
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
    public class GetUmreCountByAssociationQueryHandler : IRequestHandler<GetUmreCountByAssociationQuery, Object>
    {
        private readonly IUmreFormRepository _repo;
        private readonly IMapper _mapper;

        public GetUmreCountByAssociationQueryHandler(IUmreFormRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<Object> Handle(GetUmreCountByAssociationQuery request, CancellationToken cancellationToken)
        {
            var counts = await _repo.All
    .GroupBy(x => x.ClosestAssociation.Name)
    .Select(group => new
    {
        Association = group.Key,
        Count = group.Count()
    })
    .OrderByDescending(x => x.Count)
    .ToListAsync();

            return counts;
        }
    }
}
