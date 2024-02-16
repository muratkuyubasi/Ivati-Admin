using AutoMapper;
using ContentManagement.Common.UnitOfWork;
using ContentManagement.Data.Dto;
using ContentManagement.Domain;
using ContentManagement.Helper;
using ContentManagement.MediatR.Commands;
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
    public class UpdateDebtorsCommandHandler : IRequestHandler<UpdateDebtorsCommand, ServiceResponse<DebtorDTO>>
    {
        private readonly IDebtorRepository _debtorRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<PTContext> _uow;

        public UpdateDebtorsCommandHandler(IDebtorRepository debtorRepository, IMapper mapper, IUnitOfWork<PTContext> uow)
        {
            _debtorRepository = debtorRepository;
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<ServiceResponse<DebtorDTO>> Handle(UpdateDebtorsCommand request, CancellationToken cancellationToken)
        {
            var debtor = await _debtorRepository.FindBy(x => x.FamilyId == request.FamilyId && x.DebtorNumber == request.DebtorNumber).FirstOrDefaultAsync();
            if (debtor.IsPayment == true)
            {
                return ServiceResponse<DebtorDTO>.Return409("Bu aileye ait borç ödenmiştir!");
            }
            debtor.Amount = (decimal)request.Amount;
            debtor.DueDate =  request.DueDate;
            debtor.DebtorTypeId = request.DebtorTypeId;
            _debtorRepository.Update(debtor);
            if (await _uow.SaveAsync()<=0)
            {
                return ServiceResponse<DebtorDTO>.Return409("İşlem gerçekleşirken bir hata oldu!");
            }
            else
                return ServiceResponse<DebtorDTO>.ReturnResultWith200(_mapper.Map<DebtorDTO>(debtor));
        }
    }
}
