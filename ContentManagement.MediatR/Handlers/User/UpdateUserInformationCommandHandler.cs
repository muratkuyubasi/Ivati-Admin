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
    public class UpdateUserInformationCommandHandler : IRequestHandler<UpdateUserInformationCommand, ServiceResponse<UpdateUserContactDTO>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<PTContext> _uow;
        private readonly IAddressRepository _addressRepository;
        private readonly IFamilyMemberRepository _familyMemberRepository;
        private readonly UserManager<User> _userManager;
        private readonly IFamilyRepository _familyRepository;
        public UpdateUserInformationCommandHandler(IUserRepository userRepository, IMapper mapper, IUnitOfWork<PTContext> unitOfWork, IAddressRepository addressRepository, IFamilyMemberRepository familyMemberRepository, UserManager<User> userManager, IFamilyRepository familyRepository)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _uow = unitOfWork;
            _addressRepository = addressRepository;
            _familyMemberRepository = familyMemberRepository;
            _userManager = userManager;
            _familyRepository = familyRepository;
        }
        public async Task<ServiceResponse<UpdateUserContactDTO>> Handle(UpdateUserInformationCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindBy(x => x.Id == request.UserId).AsNoTracking().FirstOrDefaultAsync();
            if (user == null)
            {
                return ServiceResponse<UpdateUserContactDTO>.Return500();
            }
            #region Kullanıcı adı ve Şifre Düzenleme
            //user.UserName = request.UserName;
            string code = await _userManager.GeneratePasswordResetTokenAsync(user);
            IdentityResult passwordResult = await _userManager.ResetPasswordAsync(user, code, request.Password);
            if (!passwordResult.Succeeded)
            {
                return ServiceResponse<UpdateUserContactDTO>.Return500();
            }
            #endregion
            user.Email = request.Email;
            user.PhoneNumber = request.PhoneNumber;
            user.ModifiedDate = DateTime.Now;
            var fam = _familyRepository.FindBy(x => x.UserId == user.Id).FirstOrDefault();
            var address = _addressRepository.FindBy(X => X.FamilyId == fam.Id).AsNoTracking().FirstOrDefault();
            if (address != null)
            {
                address.Street = request.Street;
                address.PostCode = request.PostCode;
                address.District = request.District;
                address.Email = request.Email;
                address.PhoneNumber = request.PhoneNumber;

                _addressRepository.Update(address);
            }
            else
            {
                var fmaddress = new Address
                {
                    District = request.District,
                    Street = request.Street,
                    PostCode = request.PostCode,
                    PhoneNumber = request.PhoneNumber,
                    Email = request.Email,
                    FamilyId = user.Family.Id,
                };
                _addressRepository.Add(address);
            }
            _userRepository.Update(user);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<UpdateUserContactDTO>.Return409("Güncelleme işlemi sırasında bir hata meydana geldi!");
            }
            else return ServiceResponse<UpdateUserContactDTO>.ReturnResultWith200(_mapper.Map<UpdateUserContactDTO>(user));
        }
    }
}
