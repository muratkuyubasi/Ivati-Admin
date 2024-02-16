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
    public class GetUnpaidDebtorCountQueryHandler : IRequestHandler<GetUnpaidDebtorCountQuery, object>
    {
        private readonly IDebtorRepository _repo;
        private readonly IMapper _mapper;

        public GetUnpaidDebtorCountQueryHandler(IDebtorRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<object> Handle(GetUnpaidDebtorCountQuery request, CancellationToken cancellationToken)
        {
            var count = await _repo.All.GroupBy(x => x.DebtorType.Name).Select(x => new
            {
                Name = x.Key,
                Count = x.Count(x => x.IsPayment == false)
            }).OrderByDescending(x => x.Count).ToListAsync();
            return count;
        }
    }
}
