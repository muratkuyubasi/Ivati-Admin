using AutoMapper;
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
    public class GetCountByAssociationQueryHandler : IRequestHandler<GetCountByAssociationQuery, Object>
    {
        private readonly IHacRepository _repo;
        private readonly IMapper _mapper;

        public GetCountByAssociationQueryHandler(IHacRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<Object> Handle(GetCountByAssociationQuery request, CancellationToken cancellationToken)
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
