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
    public class UpdateFamilyAddressCommandHandler : IRequestHandler<UpdateFamilyAddressCommand, ServiceResponse<AddressDTO>>
    {
        private readonly IAddressRepository _addressRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<PTContext> _uow;

        public UpdateFamilyAddressCommandHandler(IAddressRepository addressRepository, IMapper mapper, IUnitOfWork<PTContext> uow)
        {
            _addressRepository = addressRepository;
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<ServiceResponse<AddressDTO>> Handle(UpdateFamilyAddressCommand request, CancellationToken cancellationToken)
        {
            var address = _addressRepository.FindBy(x => x.FamilyId == request.FamilyId).FirstOrDefault();
            if (address == null)
            {
                return ServiceResponse<AddressDTO>.Return409("Bu ID'ye ait bir adres bulunamadı!");
            }
            address.Street = request.Street;
            address.District = request.District;
            address.Email = request.Email;
            address.PhoneNumber = request.PhoneNumber;
            address.PostCode = request.PostCode;
            _addressRepository.Update(address);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<AddressDTO>.Return409("Güncelleme işlemi sırasında bir hata meydana geldi!");
            }
            else return ServiceResponse<AddressDTO>.ReturnResultWith200(_mapper.Map<AddressDTO>(address));
        }
    }
}
