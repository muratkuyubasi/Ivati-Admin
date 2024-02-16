using AutoMapper;
using ContentManagement.Common.UnitOfWork;
using ContentManagement.Data;
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
    internal class DeleteFamilyMemberCommandHandler : IRequestHandler<DeleteFamilyMemberCommand, ServiceResponse<UserInformationDTO>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<PTContext> _uow;
        private readonly IFamilyMemberRepository _familyMemberRepository;
        private readonly IFamilyRepository _familyRepository;

        public DeleteFamilyMemberCommandHandler(IUserRepository userRepository, IMapper mapper, IUnitOfWork<PTContext> uow, IFamilyMemberRepository familyMemberRepository, IFamilyRepository familyRepository)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _uow = uow;
            _familyMemberRepository = familyMemberRepository;
            _familyRepository = familyRepository;
        }

        public async Task<ServiceResponse<UserInformationDTO>> Handle(DeleteFamilyMemberCommand request, CancellationToken cancellationToken)
        {
            var user = _userRepository.All.Include(x=>x.Family).Include(x=>x.FamilyMembers).Where(x=>x.Id == request.Id).FirstOrDefault();
            if (user == null)
            {
                return ServiceResponse<UserInformationDTO>.Return409("Bu ID'ye ait bir kullanıcı bulunmamaktadır!");
            }
            var f = _familyRepository.All.Include(x=>x.FamilyMembers).Where(x => x.UserId == user.Id).AsNoTracking().FirstOrDefault();
            if (f != null)
            {
                var fm = f.FamilyMembers.Where(x => x.MemberTypeId == 2).FirstOrDefault();
                fm.MemberTypeId = 1;
                f.UserId = fm.MemberUserId;
                _familyRepository.Update(f);
                _familyMemberRepository.Update(fm);
            }
            else _userRepository.DeleteUser(user.Id);

            //fm.MemberUser.MemberTypeId = 1;
            //_userRepository.Update(fm.MemberUser);

            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<UserInformationDTO>.Return409("Silme işlemi sırasında bir hata meydana geldi!");
            }
            else return ServiceResponse<UserInformationDTO>.ReturnResultWith200(_mapper.Map<UserInformationDTO>(user));
        }
    }
}
