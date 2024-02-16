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
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ContentManagement.MediatR.Handlers
{
    public class UpdateUmreFormCommandHandler : IRequestHandler<UpdateUmreFormCommand, ServiceResponse<UmreFormDTO>>
    {
        private readonly IUmreFormRepository _umreFormRepository;
        private readonly IUnitOfWork<PTContext> _uow;
        private readonly IMapper _mapper;

        public UpdateUmreFormCommandHandler(IUmreFormRepository umreFormRepository, IMapper mapper, IUnitOfWork<PTContext> uow)
        {
            _umreFormRepository = umreFormRepository;
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<UmreFormDTO>> Handle(UpdateUmreFormCommand request, CancellationToken cancellationToken)
        {
            var form = await _umreFormRepository.FindBy(x => x.Id == request.Id).FirstOrDefaultAsync();
            if (form == null)
            {
                return ServiceResponse<UmreFormDTO>.Return409("Bu ID'ye ait bir başvuru bulunamadı!");
            }
            form.RoomTypeId = request.RoomTypeId;
            form.ClosestAssociationId = request.ClosestAssociationId;
            form.Surname = request.Surname;
            form.Name   = request.Name;
            form.FatherName = request.FatherName;
            form.MotherName = request.MotherName;
            form.PlaceOfBirth = request.PlaceOfBirth;
            form.TurkeyIdentificationNumber = request.TurkeyIdentificationNumber;
            form.GenderId = request.GenderId;
            form.MaritalStatusId = request.MaritalStatusId;
            form.DateOfBirth = request.DateOfBirth;
            form.Nationality = request.Nationality;
            form.PassportNumber = request.PassportNumber;
            form.PassportGivenDate = request.PassportGivenDate;
            form.PassportGivenPlace = request.PassportGivenPlace;
            form.PassportExpirationDate = request.PassportExpirationDate;
            form.Address = request.Address;
            form.City = request.City;
            form.PostCode = request.PostCode;
            form.Country = request.Country;
            form.PhoneNumber = request.PhoneNumber;
            form.DepartureAirportId = request.DepartureAirportId;
            form.LandingAirportId = request.LandingAirportId;
            form.Explanation = request.Explanation;
            form.PassportPicture = request.PassportPicture;
            form.HeadshotPicture = request.HeadshotPicture;

            _umreFormRepository.Update(form);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<UmreFormDTO>.Return409("Güncelleme işlemi sırasında bir hata oluştu!");
            }
            else
                return ServiceResponse<UmreFormDTO>.ReturnResultWith200(_mapper.Map<UmreFormDTO>(form));

        }
    }
}
