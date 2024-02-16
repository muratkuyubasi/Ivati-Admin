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
    public class ReportFamilyMemberDateOfDeathCommandHandler : IRequestHandler<ReportFamilyMemberDateOfDeathCommand, ServiceResponse<UserInformationDTO>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<PTContext> _uow;

        public ReportFamilyMemberDateOfDeathCommandHandler(IUserRepository userRepository, IMapper mapper, IUnitOfWork<PTContext> uow)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<ServiceResponse<UserInformationDTO>> Handle(ReportFamilyMemberDateOfDeathCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindBy(x => x.Id == request.Id).FirstOrDefaultAsync();
            if (user == null)
            {
                return ServiceResponse<UserInformationDTO>.Return409("Bu ID'ye ait bir kullanıcı bulunamadı!");
            }
            user.IsDead = true;
            user.DateOfDeath = request.DateOfDeath.Date;
            user.PlaceOfDeath = request.PlaceOfDeath;
            user.BurialPlace= request.BurialPlace;
            _userRepository.Update(user);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<UserInformationDTO>.Return409("İşlem sırasında bir hata meydana geldi!");
            }
            else return ServiceResponse<UserInformationDTO>.ReturnResultWith200(_mapper.Map<UserInformationDTO>(user));
        }
    }
}
