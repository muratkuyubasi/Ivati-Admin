using AutoMapper;
using CenazeFonu.Common.UnitOfWork;
using CenazeFonu.Data.Dto;
using CenazeFonu.Data.Models;
using CenazeFonu.Domain;
using CenazeFonu.Helper;
using CenazeFonu.MediatR.Commands;
using CenazeFonu.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CenazeFonu.MediatR.Handlers
{
    public class AddDebtorCommandHandler : IRequestHandler<AddDebtorCommand, ServiceResponse<DebtorDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IDebtorRepository _debtorRepo;
        private readonly IUnitOfWork<PTContext> _uow;
        private readonly IFamilyRepository _familyRepo;
        private readonly IDebtorTypeRepository _debtorTypeRepo;
        public AddDebtorCommandHandler(IMapper mapper, IDebtorRepository debtorRepo, IUnitOfWork<PTContext> uow, IFamilyRepository familyRepo, IDebtorTypeRepository debtorTypeRepo)
        {
            _mapper = mapper;
            _debtorRepo = debtorRepo;
            _uow = uow;
            _familyRepo = familyRepo;
            _debtorTypeRepo = debtorTypeRepo;
        }

        public async Task<ServiceResponse<DebtorDTO>> Handle(AddDebtorCommand request, CancellationToken cancellationToken)
        {
            var debtor = _mapper.Map<Debtor>(request);
            debtor.Id = Guid.NewGuid();
            debtor.IsPayment = false;
            debtor.DebtorNumber = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 12);
            debtor.CreationDate = DateTime.Now;
            debtor.IsDeleted = false;
            var family = _familyRepo.FindBy(x=>x.Id == debtor.FamilyId).FirstOrDefault();
            if (family == null)
            {
                return ServiceResponse<DebtorDTO>.Return409("Bu ID'ye ait bir aile bulunmamaktadır!");
            }
            var type = _debtorTypeRepo.FindBy(x=>x.Id == request.DebtorTypeId).FirstOrDefault();
            if (type == null)
            {
                return ServiceResponse<DebtorDTO>.Return409("Bu ID'ye ait bir fatura tipi bulunmamaktadır!");
            }
            _debtorRepo.Add(debtor);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<DebtorDTO>.Return409("Ekleme işlemi sırasında bir hata meydana geldi!");
            }
            else return ServiceResponse<DebtorDTO>.ReturnResultWith200(_mapper.Map<DebtorDTO>(debtor));
        }
    }
}
