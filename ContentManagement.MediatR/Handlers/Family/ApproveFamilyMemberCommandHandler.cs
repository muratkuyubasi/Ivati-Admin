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
    public class ApproveFamilyMemberCommandHandler : IRequestHandler<ApproveFamilyMemberCommand, ServiceResponse<UserInformationDTO>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<PTContext> _uow;

        public ApproveFamilyMemberCommandHandler(IUserRepository userRepository, IMapper mapper, IUnitOfWork<PTContext> uow)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<ServiceResponse<UserInformationDTO>> Handle(ApproveFamilyMemberCommand request, CancellationToken cancellationToken)
        {
            var user = _userRepository.FindBy(x=>x.Id == request.Id).FirstOrDefault();
            if (user == null)
            {
                return ServiceResponse<UserInformationDTO>.Return409("Bu ID'ye ait bir aile üyesi bulunamadı!");
            }
            user.IsActive = true;
            _userRepository.Update(user);
            //_uow.Context.Users.Attach(user).Property(x=>x.IsActive).CurrentValue = true;
            if (await _uow.SaveAsync()<=0)
            {
                return ServiceResponse<UserInformationDTO>.Return409("Kullanıcıyı onaylama işlemi sırasında bir hata meydana geldi!");
            }
            else return ServiceResponse<UserInformationDTO>.ReturnResultWith200(_mapper.Map<UserInformationDTO>(user));
        }
    }
}
