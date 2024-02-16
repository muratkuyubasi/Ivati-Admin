using AutoMapper;
using Hafiz.Core.Utilities.Mail;
using ContentManagement.Common.UnitOfWork;
using ContentManagement.Data;
using ContentManagement.Data.Dto;
using ContentManagement.Data.Models;
using ContentManagement.Domain;
using ContentManagement.Helper;
using ContentManagement.MediatR.Commands;
using ContentManagement.Repository;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ContentManagement.MediatR.Handlers
{
    internal class ChangeFamilyActivityStatusCommandHandler : IRequestHandler<ChangeFamilyActivityStatusCommand, ServiceResponse<FamilyDTO>>
    {
        private readonly IFamilyRepository _familyRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<PTContext> _uow;
        private readonly IUserRepository _userRepository;
        private readonly IDebtorRepository _debtorRepository;
        private readonly UserManager<User> _userManager;

        public ChangeFamilyActivityStatusCommandHandler(IFamilyRepository familyRepository, IMapper mapper, IUnitOfWork<PTContext> uow, IUserRepository userRepository, IDebtorRepository debtorRepository, UserManager<User> userManager)
        {
            _familyRepository = familyRepository;
            _mapper = mapper;
            _uow = uow;
            _userRepository = userRepository;
            _debtorRepository = debtorRepository;
            _userManager = userManager;
        }

        public async Task<ServiceResponse<FamilyDTO>> Handle(ChangeFamilyActivityStatusCommand request, CancellationToken cancellationToken)
        {
            var family = _familyRepository.All.Include(x=>x.Debtors).Include(x=>x.Address)
                .Include(X=>X.FamilyMembers).ThenInclude(X=>X.MemberUser)
                .Where(x=>x.Id == request.FamilyId && x.IsDeleted == false)
                .FirstOrDefault();
            if (family == null)
            {
                return ServiceResponse<FamilyDTO>.Return409("Bu ID'ye ait bir aile bulunamadı!");
            }
            family.IsActive = true;
            _familyRepository.Update(family);
            foreach (var member in family.FamilyMembers)
            {
                var m = _userRepository.Find(member.MemberUserId);
                m.IsActive = true;
                _userRepository.Update(m);
                //_uow.Context.Users.Attach(member.MemberUser).Property(x => x.IsActive).CurrentValue = true;
            }
            foreach (var debtor in family.Debtors)
            {
                debtor.IsPayment = true;
                debtor.PaymentDate = DateTime.Now;
                _debtorRepository.Update(debtor);
            }
            //_uow.Context.Families.Attach(family).Property(x=>x.IsActive).CurrentValue = true;
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<FamilyDTO>.Return409("Onaylama işlemi sırasında bir hata meydana geldi!");
            }
            else
            {
                var fm = family.FamilyMembers.Where(x => x.MemberTypeId == 1).FirstOrDefault();
                string numara = fm.MemberUser.IdentificationNumber;
                string sifre = numara.Substring(0, numara.Length >= 6 ? 6 : numara.Length);
                string code = await _userManager.GeneratePasswordResetTokenAsync(fm.MemberUser);
                IdentityResult passwordResult = await _userManager.ResetPasswordAsync(fm.MemberUser, code, sifre);
                if (!passwordResult.Succeeded)
                {
                    return ServiceResponse<FamilyDTO>.Return500();
                }
                var message = "İsveç Cenaze Fonuna başvurunuz onaylanmıştır, admin paneline giriş bilgileriniz; \n" + "Kullanıcı Adı: " + fm.MemberUser.UserName + "\n" + "Şifre: " + sifre;
                var subject = "İsveç Cenaze Fonu - Onaylanma Durumu Bilgilendirme";
                MailHelper.SendMail(message, subject, family.Address.Email);
                return ServiceResponse<FamilyDTO>.ReturnResultWith200(_mapper.Map<FamilyDTO>(family));
            }

        }
    }
}
