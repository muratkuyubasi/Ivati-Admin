using AutoMapper;
using CenazeFonu.Data.Dto;
using CenazeFonu.MediatR.Queries;
using CenazeFonu.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CenazeFonu.Helper;
using System.Linq.Dynamic.Core;
using System.Linq;

namespace CenazeFonu.MediatR.Handlers
{
    public class GetAllNonPaymentDebtorQueryHandler : IRequestHandler<GetAllNonPaymentDebtorsQuery, ServiceResponse<List<DebtorDTO>>>
    {
        private readonly IDebtorRepository _debtorRepository;
        private readonly IMapper _mapper;

        public GetAllNonPaymentDebtorQueryHandler(
            IDebtorRepository debtorRepository,
            IMapper mapper)
        {
            _debtorRepository = debtorRepository;
            _mapper = mapper;

        }
        public async Task<ServiceResponse<List<DebtorDTO>>> Handle(GetAllNonPaymentDebtorsQuery request, CancellationToken cancellationToken)
        {
            var entities = await _debtorRepository.All.Where(x=>x.IsPayment == false).ToListAsync();
            return ServiceResponse<List<DebtorDTO>>.ReturnResultWith200(_mapper.Map<List<DebtorDTO>>(entities));
        }
    }
}
