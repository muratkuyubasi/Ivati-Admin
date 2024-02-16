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
    public class UpdateHacFormCommandHandler : IRequestHandler<UpdateHacFormCommand, ServiceResponse<HacFormDTO>>
    {
        private readonly IHacRepository _hacRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<PTContext> _uow;

        public UpdateHacFormCommandHandler(IHacRepository hacRepository, IMapper mapper, IUnitOfWork<PTContext> uow)
        {
            _hacRepository = hacRepository;
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<ServiceResponse<HacFormDTO>> Handle(UpdateHacFormCommand request, CancellationToken cancellationToken)
        {
            var form = await _hacRepository.FindBy(x => x.Id == request.Id).FirstOrDefaultAsync();
            if (form == null)
            {
                return ServiceResponse<HacFormDTO>.Return409("Bu ID'ye ait bir başvuru bulunamadı!");
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

            _hacRepository.Update(form);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<HacFormDTO>.Return409("Güncelleme işlemi sırasında bir hata oluştu!");
            }
            else
                return ServiceResponse<HacFormDTO>.ReturnResultWith200(_mapper.Map<HacFormDTO>(form));

        }
    }
}
