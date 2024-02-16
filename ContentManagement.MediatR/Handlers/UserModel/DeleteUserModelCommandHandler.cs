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
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ContentManagement.MediatR.Handlers
{
    public class DeleteUserModelCommandHandler : IRequestHandler<DeleteUserModelCommand, ServiceResponse<FamilySimpleDTO>>
    {
        private readonly IFamilyRepository _familyRepository;
        private readonly IUnitOfWork<PTContext> _uow;
        private readonly IMapper _mapper;
        private readonly IReplacementIdRepository _replacementIdRepository;
        private readonly IUserRepository _userRepository;

        public DeleteUserModelCommandHandler(IFamilyRepository familyRepository, IUnitOfWork<PTContext> uow, IMapper mapper, IReplacementIdRepository replacementIdRepository, IUserRepository userRepository)
        {
            _familyRepository = familyRepository;
            _uow = uow;
            _mapper = mapper;
            _replacementIdRepository = replacementIdRepository;
            _userRepository = userRepository;
        }

        public async Task<ServiceResponse<FamilySimpleDTO>> Handle(DeleteUserModelCommand request, CancellationToken cancellationToken)
        {
            var family = _familyRepository.All.Include(x=>x.FamilyMembers).ThenInclude(x=>x.MemberUser)
                .Where(x => x.Id == request.FamilyId).FirstOrDefault();
            //family.IsDeleted = true;
            //_familyRepository.Update(family); MemberId yüzünden hata veriyor
            //var family = new Data.Models.Family { MemberId = request.Id };
            family.IsDeleted = true;
            _familyRepository.Update(family);
            //_familyRepository.Delete(family.Id);
            //_uow.Context.Families.Attach(family).Property(x => x.IsDeleted).CurrentValue = true;
            foreach (var item in family.FamilyMembers.Where(x=>x.MemberUser.IsDeleted == false).ToList())
            {
                var u = _userRepository.All.Where(x => x.Id == item.MemberUserId).AsNoTracking().FirstOrDefault();
                if (u.MemberTypeId == 1)
                {
                    u.UserName += "-";
                    u.NormalizedUserName += "-";
                    _userRepository.AileReisiUserName(u.Id);
                }
                _userRepository.DeleteUser(u.Id);
                //_uow.Context.Users.Attach(item.MemberUser).Property(x => x.IsDeleted).CurrentValue = true;
                //_userRepository.Delete(item.Id);
            }
            var a =  new Data.Models.ReplacementId
            {
                SubId = family.MemberId,
                CreationDate = DateTime.Now,
                IsActive = true,
                Id = Guid.NewGuid(),
            };
            var idcheck = _replacementIdRepository.FindBy(x => x.SubId == a.SubId && x.IsActive == false).FirstOrDefault();
            if (idcheck == null)
            {
                _replacementIdRepository.Add(a);
            }
            else
            {
                idcheck.IsActive = true;
                _replacementIdRepository.Update(idcheck);
            }
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<FamilySimpleDTO>.Return409("Silme işlemi sırasında bir hata meydana geldi!");
            }
            else return ServiceResponse<FamilySimpleDTO>.ReturnResultWith200(_mapper.Map<FamilySimpleDTO>(family));
        }
    }
}
