using AutoMapper;
using ContentManagement.Data.Dto;
using ContentManagement.Helper;
using ContentManagement.MediatR.Queries;
using ContentManagement.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ContentManagement.MediatR.Handlers
{
    public class GetDebtorByYearQueryHandler : IRequestHandler<GetDebtorByYearQuery, ServiceResponse<List<DebtorByYearDTO>>>
    {
        private readonly IDebtorRepository _debtorRepository;
        private readonly IMapper _mapper;

        public GetDebtorByYearQueryHandler(IDebtorRepository debtorRepository, IMapper mapper)
        {
            _debtorRepository = debtorRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<DebtorByYearDTO>>> Handle(GetDebtorByYearQuery request, CancellationToken cancellationToken)
        {
            int[] years = { 2018, 2019, 2020, 2021, 2022, 2023 };
            List<DebtorByYearDTO> list = new List<DebtorByYearDTO>();
            foreach (int year in years)
            {
                var data = _debtorRepository.FindBy(x => x.PaymentDate.Value.Year == year).FirstOrDefault();
                if (data != null)
                {
                    list.Add(_mapper.Map<DebtorByYearDTO>(data));
                }              
            }
            return ServiceResponse<List<DebtorByYearDTO>>.ReturnResultWith200(_mapper.Map<List<DebtorByYearDTO>>(list));
        }
    }
}
