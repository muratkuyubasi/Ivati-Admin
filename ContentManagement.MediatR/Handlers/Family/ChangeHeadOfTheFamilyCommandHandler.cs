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
    public class ChangeHeadOfTheFamilyCommandHandler : IRequestHandler<ChangeHeadOfTheFamilyCommand, ServiceResponse<FamilyMemberDTO>>
    {
        private readonly IFamilyMemberRepository _familyMemberRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<PTContext> _uow;
        private readonly IFamilyRepository _familyRepository;
        private readonly UserManager<User> _userManager;

        public ChangeHeadOfTheFamilyCommandHandler(IFamilyMemberRepository familyMemberRepository, IMapper mapper, IUnitOfWork<PTContext> uow, IUserRepository userRepository, IFamilyRepository familyRepository, UserManager<User> userManager)
        {
            _familyMemberRepository = familyMemberRepository;
            _mapper = mapper;
            _uow = uow;
            _userRepository = userRepository;
            _familyRepository = familyRepository;
            _userManager = userManager;
        }
        public async Task<ServiceResponse<FamilyMemberDTO>> Handle(ChangeHeadOfTheFamilyCommand request, CancellationToken cancellationToken)
        {
            var headoffamily = _familyMemberRepository.FindBy(x => x.FamilyId == request.FamilyId).AsNoTracking();
            if (headoffamily == null)
            {
                return ServiceResponse<FamilyMemberDTO>.Return409("Böyle bir aile bulunamadı!");
            }
            var newR = _familyMemberRepository.All.Where(x => x.MemberUserId == request.MemberId).Include(x=>x.MemberUser).AsNoTracking().FirstOrDefault();
            var r = _familyMemberRepository.All.Where(x=>x.FamilyId == request.FamilyId && x.MemberTypeId == 1).Include(x=>x.MemberUser).AsNoTracking().FirstOrDefault();

            var fam = _familyRepository.All.Where(x=>x.Id == request.FamilyId).Include(x=>x.Address).FirstOrDefault();

            //_uow.Context.Users.Attach(r.MemberUser).Property(x => x.MemberTypeId).CurrentValue = newR.MemberTypeId;
            r.MemberUser.MemberTypeId = newR.MemberTypeId;
            _userRepository.Update(r.MemberUser);

            newR.MemberUser.MemberTypeId = r.MemberTypeId;
            _userRepository.Update(newR.MemberUser);
            //_uow.Context.Users.Attach(newR.MemberUser).Property(x => x.MemberTypeId).CurrentValue =1 ;
            
            r.MemberTypeId = newR.MemberTypeId;
            _familyMemberRepository.Update(r);
            //_uow.Context.FamilyMembers.Attach(r).Property(x => x.MemberTypeId).CurrentValue = newR.MemberTypeId;

            newR.MemberTypeId = 1;
            _familyMemberRepository.Update(r);

            fam.UserId = newR.MemberUserId;
            _familyRepository.Update(fam);

            r.MemberUser.UserName = null;
            r.MemberUser.NormalizedUserName = null;
            _userRepository.Update(r.MemberUser);
            _uow.Save();

            newR.MemberUser.UserName = fam.MemberId.ToString();
            newR.MemberUser.NormalizedUserName = fam.MemberId.ToString();
            _userRepository.Update(newR.MemberUser);
            _uow.Save();

            string numara = newR.MemberUser.IdentificationNumber;
            string sifre = numara.Substring(0, numara.Length >= 6 ? 6 : numara.Length);
            //var sifre = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 8);
            string code = await _userManager.GeneratePasswordResetTokenAsync(newR.MemberUser);
            IdentityResult passwordResult = await _userManager.ResetPasswordAsync(newR.MemberUser, code, sifre);
            if (!passwordResult.Succeeded)
            {
                return ServiceResponse<FamilyMemberDTO>.Return500();
            }
            var message = "İsveç Cenaze Fonu içerisinde yapmış olduğunuz aile reisi değişikliği onaylanmıştır, yeni aile reisinin admin paneline giriş bilgileri; \n" + "Kullanıcı Adı: " + newR.Family.MemberId + "\n" + "Şifre: " + sifre;
            var subject = "İsveç Cenaze Fonu - Aile Reisi Bilgilendirme";
            MailHelper.SendMail(message, subject, fam.Address.Email);
            //_uow.Context.FamilyMembers.Attach(newR).Property(x => x.MemberTypeId).CurrentValue = 1;

            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<FamilyMemberDTO>.ReturnResultWith200(_mapper.Map<FamilyMemberDTO>(newR));
            }
            else return ServiceResponse<FamilyMemberDTO>.Return409("İşlem sırasında bir hata meydana geldi!");
        }
    }
}
