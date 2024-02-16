using AutoMapper;
using CenazeFonu.Common.UnitOfWork;
using CenazeFonu.Data.Dto;
using CenazeFonu.Domain;
using CenazeFonu.Helper;
using CenazeFonu.MediatR.Commands;
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
    public class UpdateDebtorCommandHandler : IRequestHandler<UpdateDebtorCommand, ServiceResponse<DebtorDTO>>
    {
        private readonly IDebtorRepository _debtorRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<PTContext> _uow;

        public UpdateDebtorCommandHandler(IDebtorRepository debtorRepository, IMapper mapper, IUnitOfWork<PTContext> uow)
        {
            _debtorRepository = debtorRepository;
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<ServiceResponse<DebtorDTO>> Handle(UpdateDebtorCommand request, CancellationToken cancellationToken)
        {
            var debtor = await _debtorRepository.FindBy(x => x.FamilyId == request.FamilyId && x.DebtorNumber == request.DebtorNumber).FirstOrDefaultAsync();
            if (debtor.IsPayment == true)
            {
                return ServiceResponse<DebtorDTO>.Return409("Bu aileye ait borç ödenmiştir!");
            }
            debtor.IsPayment = true;
            debtor.PaymentDate = DateTime.Now;
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
