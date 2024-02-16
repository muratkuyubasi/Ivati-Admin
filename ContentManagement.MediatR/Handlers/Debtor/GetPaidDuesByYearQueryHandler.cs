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
    public class GetPaidDuesByYearQueryHandler : IRequestHandler<GetPaidDuesByYearQuery, ServiceResponse<List<DebtorDTO>>>
    {
        private readonly IDebtorRepository _debtorRepository;
        private readonly IMapper _mapper;

        public GetPaidDuesByYearQueryHandler(IDebtorRepository debtorRepository, IMapper mapper)
        {
            _debtorRepository = debtorRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<DebtorDTO>>> Handle(GetPaidDuesByYearQuery request, CancellationToken cancellationToken)
        {
            var debtors = await _debtorRepository.FindBy(x => x.CreationDate.Year.ToString() == request.Year && x.IsPayment == true).ToListAsync();
            return ServiceResponse<List<DebtorDTO>>.ReturnResultWith200(_mapper.Map<List<DebtorDTO>>(debtors));
        }
    }
}
