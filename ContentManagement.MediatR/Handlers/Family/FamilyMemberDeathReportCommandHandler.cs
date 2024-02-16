using AutoMapper;
using ContentManagement.Common.UnitOfWork;
using ContentManagement.Data.Dto;
using ContentManagement.Domain;
using ContentManagement.Helper;
using ContentManagement.MediatR.Commands;
using ContentManagement.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ContentManagement.MediatR.Handlers
{
    public class FamilyMemberDeathReportCommandHandler : IRequestHandler<FamilyMemberDeathReportCommand, ServiceResponse<UserInformationDTO>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<PTContext> _uow;

        public FamilyMemberDeathReportCommandHandler(IUserRepository userRepository, IMapper mapper, IUnitOfWork<PTContext> uow)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<ServiceResponse<UserInformationDTO>> Handle(FamilyMemberDeathReportCommand request, CancellationToken cancellationToken)
        {
            var member = _userRepository.All.Where(x=>x.Id == request.UserId).FirstOrDefault();
            if (member == null)
            {
                return ServiceResponse<UserInformationDTO>.Return409("Bu ID'ye ait bir kullanıcı bulunamadı!");
            }

            member.IsDead = true;

            _userRepository.Update(member);

            //_uow.Context.Users.Attach(member).Property(x=>x.IsDead).CurrentValue = true;
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<UserInformationDTO>.Return409("Güncelleme işlemi sırasında bir hata meydana geldi!");
            }
            else return ServiceResponse<UserInformationDTO>.ReturnResultWith200(_mapper.Map<UserInformationDTO>(member));
        }
    }
}
