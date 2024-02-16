using ContentManagement.MediatR.Queries.Dashboard.Debtor;
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
    public class GetDebtorIncomeByYearsQueryHandler : IRequestHandler<GetDebtorIncomeByYearsQuery, Object>
    {
        private readonly IDebtorRepository _debtorRepository;

        public GetDebtorIncomeByYearsQueryHandler(IDebtorRepository debtorRepository)
        {
            _debtorRepository = debtorRepository;   
        }
        public async Task<object> Handle(GetDebtorIncomeByYearsQuery request, CancellationToken cancellationToken)
        {
            var query = await _debtorRepository.All.Where(x => x.IsPayment == true).GroupBy(x => x.PaymentDate.Value.Year)
                .Select(x => new
                {
                    Year = x.Key,
                    Income = x.Count(x=>x.Amount != 0)
                }).OrderBy(x=>x.Year).ToListAsync();
            return query;
        }
    }
}
