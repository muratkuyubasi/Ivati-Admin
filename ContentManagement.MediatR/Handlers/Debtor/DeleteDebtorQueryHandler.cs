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
    public class DeleteDebtorQueryHandler : IRequestHandler<DeleteDebtorCommand, ServiceResponse<DebtorDTO>>
    {
        private readonly IDebtorRepository _debtorRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<PTContext> _uow;

        public DeleteDebtorQueryHandler(IDebtorRepository debtorRepository, IMapper mapper, IUnitOfWork<PTContext> uow)
        {
            _debtorRepository = debtorRepository;
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<ServiceResponse<DebtorDTO>> Handle(DeleteDebtorCommand request, CancellationToken cancellationToken)
        {
            var debtor = await _debtorRepository.All.Where(x => x.FamilyId == request.FamilyId && x.DebtorNumber == request.DebtorNumber).FirstOrDefaultAsync();
            if (debtor == null)
            {
                return ServiceResponse<DebtorDTO>.Return409("Fatura Bulunamadı!");
            }
            //debtor.IsDeleted = true;
            //_debtorRepository.Update(debtor);
            _debtorRepository.Remove(debtor);
            if (await _uow.SaveAsync()<=0)
            {
                return ServiceResponse<DebtorDTO>.Return500();
            }
            else
                return ServiceResponse<DebtorDTO>.ReturnSuccess();
        }
    }
}
