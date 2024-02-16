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
    public class GetPaidDebtorCountQueryHandler : IRequestHandler<GetPaidDebtorCountQuery, Object>
    {
        private readonly IDebtorRepository _repo;

        public GetPaidDebtorCountQueryHandler(IDebtorRepository repo)
        {
            _repo = repo;
        }
        public async Task<object> Handle(GetPaidDebtorCountQuery request, CancellationToken cancellationToken)
        {
            var count = await _repo.All.GroupBy(x => x.DebtorType.Name).Select(x => new
            {
                Name = x.Key,
                Count = x.Count(x => x.IsPayment == true)
            }).OrderByDescending(x => x.Count).ToListAsync();
            return count;
        }
    }
}
