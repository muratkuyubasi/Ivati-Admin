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
    public class UpdateFamilyMemberCommandHandler : IRequestHandler<UpdateFamilyMemberCommand, ServiceResponse<UserInformationDTO>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<PTContext> _uow;

        public UpdateFamilyMemberCommandHandler(IUserRepository userRepository, IMapper mapper, IUnitOfWork<PTContext> unitOfWork)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _uow = unitOfWork;
        }
        public async Task<ServiceResponse<UserInformationDTO>> Handle(UpdateFamilyMemberCommand request, CancellationToken cancellationToken)
        {
            var member = _userRepository.All.Where(x => x.Id == request.Id).FirstOrDefault();
            if (member == null)
            {
                return ServiceResponse<UserInformationDTO>.Return409("Bu ID'ye ait bir üye bulunamadı!");
            }
            member.FirstName = request.FirstName;
            member.LastName = request.LastName;
            member.IdentificationNumber = request.IdentificationNumber;
            member.BirthPlace = request.BirthPlace;
            member.BirthDay = request.BirthDay;
            member.GenderId = request.GenderId;
            member.Nationality = request.Nationality;
            member.IsDualNationality = request.isDualNationality;
            member.MemberTypeId = request.MemberTypeId;
            member.PlaceOfDeath = request.PlaceOfDeath;
            member.DateOfDeath = request.DateOfDeath;
            member.BurialPlace = request.BurialPlace;
            _userRepository.Update(member);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<UserInformationDTO>.Return409("Güncelleme işlemi sırasında bir hata meydana geldi!");
            }
            else return ServiceResponse<UserInformationDTO>.ReturnResultWith200(_mapper.Map<UserInformationDTO>(member));
        }
    }
}
