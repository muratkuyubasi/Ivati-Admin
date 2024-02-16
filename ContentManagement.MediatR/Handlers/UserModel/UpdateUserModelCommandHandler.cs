using AutoMapper;
using ContentManagement.Common.UnitOfWork;
using ContentManagement.Data;
using ContentManagement.Data.Dto;
using ContentManagement.Data.Models;
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
    public class UpdateUserModelCommandHandler : IRequestHandler<UpdateUserModelCommand, ServiceResponse<UserModelDTO>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IAddressRepository _addressRepository;
        private readonly IFamilyMemberRepository _familyMemberRepository;
        private readonly ISpouseRepository _spouseRepository;
        private readonly IUnitOfWork<PTContext> _unitOfWork;
        private readonly IUserModelRepository _userModelRepository;

        public UpdateUserModelCommandHandler(IUserRepository userRepository, IMapper mapper, IAddressRepository addressRepository, IFamilyMemberRepository familyMemberRepository, ISpouseRepository spouseRepository, IUnitOfWork<PTContext> unitOfWork, IUserModelRepository userModelRepository)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _addressRepository = addressRepository;
            _familyMemberRepository = familyMemberRepository;
            _spouseRepository = spouseRepository;
            _unitOfWork = unitOfWork;
            _userModelRepository = userModelRepository;
        }

        public async Task<ServiceResponse<UserModelDTO>> Handle(UpdateUserModelCommand request, CancellationToken cancellationToken)
        {
            var user = _userRepository.AllIncluding(a => a.Address, s => s.Spouses, fm => fm.FamilyMembers).Where(x => x.Id == request.Id).FirstOrDefault();
            var data = _mapper.Map<UserModel>(user);
            data.FirstName = request.FirstName;
            data.LastName = request.LastName;
            data.IdentificationNumber = request.IdentificationNumber;
            data.BirthPlace = request.BirthPlace;
            data.BirthDay = request.BirthDay;
            data.Nationality = request.Nationality;
            data.AddressDTO.PostCode = request.Address.PostCode;
            data.AddressDTO.PhoneNumber = request.Address.PhoneNumber;
            data.AddressDTO.Email = request.Address.Email;
            data.AddressDTO.District = request.Address.District;
            data.AddressDTO.Street = request.Address.Street;

            foreach (var fm in data.FamilyMembers)
            {
                foreach (var fm2 in request.FamilyMembers)
                {
                    fm.FirstName = fm2.FirstName;
                    fm.LastName = fm2.LastName;
                    fm.IdentificationNumber = fm2.IdentificationNumber;
                    fm.BirthDay = fm2.BirthDay;
                    fm.BirthPlace = fm2.BirthPlace;
                    fm.Nationality = fm2.Nationality;
                    fm.IsDualNationality = fm2.IsDualNationality;
                }
            }
            _userRepository.Update(_mapper.Map<User>(data));
            if (await _unitOfWork.SaveAsync() <= 0)
            {
                return ServiceResponse<UserModelDTO>.Return409("Güncelleme işlemi sırasında bir sorun oldu!");
            }
            else return ServiceResponse<UserModelDTO>.ReturnResultWith200(_mapper.Map<UserModelDTO>(user));
        }
    }
}
