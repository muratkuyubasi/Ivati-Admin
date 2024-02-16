using AutoMapper;
using ContentManagement.Data.Dto;
using ContentManagement.MediatR.Queries;
using ContentManagement.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ContentManagement.Helper;
using System.Linq.Dynamic.Core;
using System.Linq;
using ContentManagement.Data.Models;
using ContentManagement.Common.UnitOfWork;
using ContentManagement.Domain;
using System;

namespace ContentManagement.MediatR.Handlers
{
    public class SetPenaltyFeePaymentQueryHandler : IRequestHandler<SetPenaltyFeePaymentQuery, ServiceResponse<List<DebtorDTO>>>
    {
        private readonly IDebtorRepository _debtorRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<PTContext> _uow;
        private readonly IAppSettingRepository _appSettingRepository;

        public SetPenaltyFeePaymentQueryHandler(
            IDebtorRepository debtorRepository,
            IMapper mapper, IUnitOfWork<PTContext> uow, IAppSettingRepository appSettingRepository)
        {
            _debtorRepository = debtorRepository;
            _mapper = mapper;
            _uow = uow;
            _appSettingRepository = appSettingRepository;
        }

        public async Task<ServiceResponse<List<DebtorDTO>>> Handle(SetPenaltyFeePaymentQuery request, CancellationToken cancellationToken)
        {
            var entities = await _debtorRepository.All.Where(x=>x.IsPayment == false).ToListAsync();
            var amount = _appSettingRepository.All.Where(x=>x.Key == "cezaparasi").FirstOrDefault();
            var time = _appSettingRepository.All.Where(x => x.Key == "cezazaman").FirstOrDefault();
            var intime = int.Parse(time.Value);
            foreach(var entity in entities)
            {
                entity.Amount  = entity.Amount + decimal.Parse(amount.Value);
                entity.DebtorTypeId = 2;

                var d = entity.CreationDate.AddMonths(intime);

                if (d < DateTime.Now)
                {
                    entity.Amount = entity.Amount + 150;
                }
            }
            _debtorRepository.UpdateRange(entities);
            if (await _uow.SaveAsync()<=0)
            {
                return ServiceResponse<List<DebtorDTO>>.Return500();
            }
            return ServiceResponse<List<DebtorDTO>>.ReturnResultWith200(_mapper.Map<List<DebtorDTO>>(entities));
        }
    }
}
