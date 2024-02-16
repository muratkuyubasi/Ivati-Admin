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
    public class ParentalDivorceCommandHandler : IRequestHandler<ParentalDivorceCommand, ServiceResponse<FamilyDTO>>
    {
        private readonly IFamilyRepository _familyRepository;
        private readonly IFamilyMemberRepository _memberRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<PTContext> _uow;
        private readonly IAddressRepository _addressRepository;
        private readonly IUserRepository _userRepository;

        public ParentalDivorceCommandHandler(IFamilyRepository familyRepository, IFamilyMemberRepository memberRepository, IMapper mapper, IUnitOfWork<PTContext> uow, IAddressRepository addressRepository, IUserRepository userRepository)
        {
            _familyRepository = familyRepository;
            _memberRepository = memberRepository;
            _mapper = mapper;
            _uow = uow;
            _addressRepository = addressRepository;
            _userRepository = userRepository;
        }

        public async Task<ServiceResponse<FamilyDTO>> Handle(ParentalDivorceCommand request, CancellationToken cancellationToken)
        {
            var family = _familyRepository.All.Where(x=>x.Id == request.FamilyId).Include(x=>x.Address).Include(x=>x.FamilyMembers).ThenInclude(x=>x.MemberUser).FirstOrDefault();
            var firstMember = family.FamilyMembers.Where(x => x.FamilyId == family.Id && x.MemberTypeId == 1).FirstOrDefault();
            firstMember.IsDivorced = true;
            var secondMember = family.FamilyMembers.Where(x => x.FamilyId == family.Id && x.MemberTypeId == 2).FirstOrDefault();
            secondMember.IsDivorced = true;
            int id = 0;
            var famid = _familyRepository.All.OrderBy(x=>x.MemberId).LastOrDefault();
            id = famid.MemberId + 1;
            var newFamily = new Data.Models.Family
            {
                Id = Guid.NewGuid(),
                Name = secondMember.MemberUser.LastName,
                UserId = secondMember.MemberUser.Id,
                IsDeleted = false,
                IsActive = true,
                MemberId = id,
                ReferenceNumber = famid.ReferenceNumber + famid.ReferenceNumber,
            };
            _familyRepository.Add(newFamily);

            var newFamMember = new Data.Models.FamilyMember
            {
                Id = Guid.NewGuid(),
                FamilyId = newFamily.Id,
                MemberUserId = secondMember.MemberUser.Id,
                //MemberUser = secondMember.MemberUser,
                MemberTypeId = 1,
                IsDivorced = true,
            };
            _memberRepository.Add(newFamMember);

            var newFamAddress = new Data.Models.Address
            {
                FamilyId = newFamily.Id,
                Street = family.Address.Street,
                PostCode = family.Address.PostCode,
                District = family.Address.District,
                Email = family.Address.Email,
                PhoneNumber = family.Address.PhoneNumber,
            };
            _addressRepository.Add(newFamAddress);
            secondMember.MemberUser.MemberTypeId = 1;
            _userRepository.Update(secondMember.MemberUser);
            _memberRepository.Remove(secondMember);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<FamilyDTO>.Return409("İşlem sırasında bir hata meydana geldi!");
            }
            else return ServiceResponse<FamilyDTO>.ReturnResultWith200(_mapper.Map<FamilyDTO>(newFamily));
        }
    }
}
