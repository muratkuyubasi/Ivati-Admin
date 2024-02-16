using ContentManagement.Data.Dto;
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
    public class GetIncomeByCityQueryHandler : IRequestHandler<GetIncomeByCityQuery, Object>
    {
        private readonly IDebtorRepository _debtorRepository;

        public GetIncomeByCityQueryHandler(IDebtorRepository debtorRepository)
        {
            _debtorRepository = debtorRepository;
        }
        public async Task<object> Handle(GetIncomeByCityQuery request, CancellationToken cancellationToken)
        {
            var debtors = _debtorRepository.All
               .Include(x => x.Family)
               .ThenInclude(x => x.City)
               .Where(X=>X.IsPayment == true && X.Family.CityId == request.CityId)
               .GroupBy(X=>X.CreationDate.Year)
               .Select(y => new 
               {
                   Year = y.Key,
                   Income = y.Count(x => x.Amount != 0)
               }).OrderBy(X=>X.Year).ToList();
            return debtors;
        }
    }
}
