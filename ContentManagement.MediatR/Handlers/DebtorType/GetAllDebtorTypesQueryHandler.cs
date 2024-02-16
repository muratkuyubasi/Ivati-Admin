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
    public class GetAllDebtorTypesQueryHandler : IRequestHandler<GetAllDebtorTypesQuery, ServiceResponse<List<DebtorTypeDTO>>>
    {
        private readonly IDebtorTypeRepository _debtorTypeRepository;
        private readonly IMapper _mapper;

        public GetAllDebtorTypesQueryHandler(IDebtorTypeRepository debtorTypeRepository, IMapper mapper)
        {
            _debtorTypeRepository = debtorTypeRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<DebtorTypeDTO>>> Handle(GetAllDebtorTypesQuery request, CancellationToken cancellationToken)
        {
            var list = await _debtorTypeRepository.All.ToListAsync();
            return ServiceResponse<List<DebtorTypeDTO>>.ReturnResultWith200(_mapper.Map<List<DebtorTypeDTO>>(list));
        }
    }
}
