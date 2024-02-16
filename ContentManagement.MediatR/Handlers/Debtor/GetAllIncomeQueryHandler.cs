using AutoMapper;
using ContentManagement.Data.Dto;
using ContentManagement.Helper;
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
    public class GetAllIncomeQueryHandler : IRequestHandler<GetAllIncomeByYearQuery, ServiceResponse<int>>
    {
        private readonly IDebtorRepository _debtorRepository;
        private readonly IMapper _mapper;

        public GetAllIncomeQueryHandler(IDebtorRepository debtorRepository, IMapper mapper)
        {
            _debtorRepository = debtorRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<int>> Handle(GetAllIncomeByYearQuery request, CancellationToken cancellationToken)
        {
            var incomes = await _debtorRepository.All.Where(x => x.CreationDate.Year.ToString() == request.Year && x.IsPayment == true).ToListAsync();
            var inc = 0;
            foreach (var income in incomes)
            {
                inc = (int)(inc + income.Amount);
            }
            return ServiceResponse<int>.ReturnResultWith200(inc);
        }
    }
}
