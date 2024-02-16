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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ContentManagement.MediatR.Handlers
{
    public class AddNewFamilyMemberCommandHandler : IRequestHandler<AddNewFamilyMemberCommand, ServiceResponse<FamilyMemberDTO>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IFamilyMemberRepository _familyMemberRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<PTContext> _uow;

        public AddNewFamilyMemberCommandHandler(IUserRepository userRepository, IFamilyMemberRepository familyMemberRepository, IMapper mapper, IUnitOfWork<PTContext> uow)
        {
            _userRepository = userRepository;
            _familyMemberRepository = familyMemberRepository;
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<ServiceResponse<FamilyMemberDTO>> Handle(AddNewFamilyMemberCommand request, CancellationToken cancellationToken)
        {
            var model = _mapper.Map<User>(request.MemberUser);
            var password = Guid.NewGuid().ToString();
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
            var user = new Data.User
            {
                Id = Guid.NewGuid(),
                FirstName = model.FirstName,
                LastName = model.LastName,
                IsActive = false,
                IsDeleted = false,
                CreatedDate = DateTime.Now.ToLocalTime(),
                ModifiedDate = DateTime.Now.ToLocalTime(),
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                TwoFactorEnabled = true,
                LockoutEnabled = true,
                AccessFailedCount = 0,
                GenderId = model.GenderId,
                Nationality = model.Nationality,
                IdentificationNumber = model.IdentificationNumber,
                BirthPlace = model.BirthPlace,
                BirthDay = model.BirthDay,
                IsDead = false,
                PasswordHash = passwordHash.ToString(),
                Email = model.FirstName.ToLower() + "@" + model.LastName.ToLower() + ".com",
                MemberTypeId = model.MemberTypeId,
            };
            _userRepository.Add(user);
            if (await _uow.SaveAsync()<=0)
            {
                return ServiceResponse<FamilyMemberDTO>.Return409("Bir hata meydana geldi!");
            }

            var fm = new Data.Models.FamilyMember
            {
                Id = Guid.NewGuid(),
                FamilyId = request.FamilyId,
                MemberUserId = user.Id,
                MemberTypeId = user.MemberTypeId
            };
            _familyMemberRepository.Add(fm);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<FamilyMemberDTO>.Return409("Ekleme işlemi sırasında bir hata meydana geldi!");
            }
            else return ServiceResponse<FamilyMemberDTO>.ReturnResultWith200(_mapper.Map<FamilyMemberDTO>(fm));
        }
    }
}
