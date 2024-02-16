using AutoMapper;
using CenazeFonu.Data.Dto;
using CenazeFonu.Helper;
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
    public class GetFamilyDebtorQueryHandler : IRequestHandler<GetFamilyDebtorQuery, ServiceResponse<DebtorFamilyDTO>>
    {
        private readonly IDebtorRepository _debtorRepository;
        private readonly IMapper _mapper;

        public GetFamilyDebtorQueryHandler(IDebtorRepository debtorRepository, IMapper mapper)
        {
            _debtorRepository = debtorRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<DebtorFamilyDTO>> Handle(GetFamilyDebtorQuery request, CancellationToken cancellationToken)
        {
            var debtor = await _debtorRepository.FindBy(x => x.FamilyId == request.FamilyId).FirstOrDefaultAsync();
            return ServiceResponse<DebtorFamilyDTO>.ReturnResultWith200(_mapper.Map<DebtorFamilyDTO>(debtor));
        }
    }
}
