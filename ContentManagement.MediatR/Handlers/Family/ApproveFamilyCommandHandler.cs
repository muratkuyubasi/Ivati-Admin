using AutoMapper;
using ContentManagement.Common.UnitOfWork;
using ContentManagement.Data;
using ContentManagement.Data.Dto;
using ContentManagement.Domain;
using ContentManagement.Helper;
using ContentManagement.MediatR.Commands;
using ContentManagement.Repository;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ContentManagement.MediatR.Handlers
{
    public class ApproveFamilyCommandHandler : IRequestHandler<ApproveFamilyCommand, ServiceResponse<FamilyDTO>>
    {
        private readonly IFamilyRepository _familyRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<PTContext> _uow;
        private readonly IUserRepository _userRepository;
        private readonly IDebtorRepository _debtorRep;

        public ApproveFamilyCommandHandler(IFamilyRepository familyRepository, IMapper mapper, IUnitOfWork<PTContext> uow, IUserRepository userRepository, IDebtorRepository debtorRep)
        {
            _familyRepository = familyRepository;
            _mapper = mapper;
            _uow = uow;
            _userRepository = userRepository;
            _debtorRep = debtorRep;
        }

        public async Task<ServiceResponse<FamilyDTO>> Handle(ApproveFamilyCommand request, CancellationToken cancellationToken)
        {
            var family = _familyRepository.All.Include(x=>x.Address).Include(x=>x.Debtors)
                .Include(x=>x.FamilyMembers).ThenInclude(x=>x.MemberUser).Where(x=>x.MemberId == request.MemberId).FirstOrDefault();
            if (family == null)
            {
                return ServiceResponse<FamilyDTO>.Return409("Bu ID'ye ait bir aile bulunamadı!");
            }
            family.IsActive = true;
            foreach (var item in family.FamilyMembers)
            {
                var member = _userRepository.FindBy(x=>x.Id == item.MemberUserId).FirstOrDefault();
                member.IsActive = true;
                _userRepository.Update(member);
                //item.MemberUser.IsActive = true;
                //_uow.Context.FamilyMembers.Attach(item).Property(x => x.MemberUser.IsActive).CurrentValue = true; MemberUser alanına ulaşamadığı için patladı
                //_uow.Context.FamilyMembers.Update(item); // Tek bir alan güncelleyemediği için patladı
            }
            foreach (var deb in family.Debtors)
            {
                var firstDebtor = _debtorRep.FindBy(x => x.DebtorNumber == deb.DebtorNumber).FirstOrDefault();
                firstDebtor.IsPayment = true;
                _debtorRep.Update(firstDebtor);
            }
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<FamilyDTO>.Return409("Onaylama işlemi sırasında bir hata meydana geldi!");
            }
            else return ServiceResponse<FamilyDTO>.ReturnResultWith200(_mapper.Map<FamilyDTO>(family));
        }
    }
}

