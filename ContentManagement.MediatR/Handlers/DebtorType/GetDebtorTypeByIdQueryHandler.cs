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
    public class GetDebtorTypeByIdQueryHandler : IRequestHandler<GetDebtorTypeByIdQuery, ServiceResponse<DebtorTypeDTO>>
    {
        private readonly IDebtorTypeRepository _debtorTypeRepository;
        private readonly IMapper _mapper;

        public GetDebtorTypeByIdQueryHandler(IDebtorTypeRepository debtorTypeRepository, IMapper mapper)
        {
            _debtorTypeRepository = debtorTypeRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<DebtorTypeDTO>> Handle(GetDebtorTypeByIdQuery request, CancellationToken cancellationToken)
        {
            var data = await _debtorTypeRepository.FindBy(x=>x.Id == request.Id).FirstOrDefaultAsync();
            if (data == null)
            {
                return ServiceResponse<DebtorTypeDTO>.Return409("Bu ID'ye ait bir fatura tipi bulunmamaktadır!");
            }
            else return ServiceResponse<DebtorTypeDTO>.ReturnResultWith200(_mapper.Map<DebtorTypeDTO>(data));
        }
    }
}
