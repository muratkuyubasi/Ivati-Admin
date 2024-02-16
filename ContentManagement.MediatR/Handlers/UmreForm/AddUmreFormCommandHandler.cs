using AutoMapper;
using ContentManagement.Common.UnitOfWork;
using ContentManagement.Data.Dto;
using ContentManagement.Data.Models;
using ContentManagement.Domain;
using ContentManagement.Helper;
using ContentManagement.MediatR.Commands;
using ContentManagement.Repository;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ContentManagement.MediatR.Handlers
{
    public class AddUmreFormCommandHandler : IRequestHandler<AddUmreFormCommand, ServiceResponse<UmreFormDTO>>
    {
        private readonly IUmreFormRepository _formRepository;
        private readonly IAssociationRepository _associationRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<PTContext> _uow;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _env;
        private readonly IAirportRepository _airportRepository;
        private readonly IUmrePeriodRepository _umrePeriodRepository;

        public AddUmreFormCommandHandler(IUmreFormRepository formRepository, IMapper mapper, IAssociationRepository associationRepository, IUnitOfWork<PTContext> uow, Microsoft.AspNetCore.Hosting.IHostingEnvironment env, IAirportRepository airportRepository, IUmrePeriodRepository uowPeriodRepository)
        {
            _formRepository = formRepository;
            _mapper = mapper;
            _associationRepository = associationRepository;
            _uow = uow;
            _env = env;
            _airportRepository = airportRepository;
            _umrePeriodRepository = uowPeriodRepository;
        }

        public async Task<ServiceResponse<UmreFormDTO>> Handle(AddUmreFormCommand request, CancellationToken cancellationToken)
        {
            var form = _mapper.Map<UmreForm>(request);
            var period = _umrePeriodRepository.FindBy(x=>x.IsActive == true).FirstOrDefault();
            if (period != null)
            {
                form.PeriodId = period.Id;
            }
            else form.PeriodId = 0;
            var user = _formRepository.FindBy(x=>x.TurkeyIdentificationNumber == form.TurkeyIdentificationNumber).FirstOrDefault();
            if (user != null)
            {
                return ServiceResponse<UmreFormDTO>.Return409("Bu kimlik numaralarına ait bir başvuru yapılmıştır!");
            }
            //var period = _umrePeriodRepository.FindBy(x => x.Id == form.Id && x.IsActive == true).FirstOrDefault();
            //if (period == null)
            //{
            //    return ServiceResponse<UmreFormDTO>.Return409("Bu numaraya ait aktif bir dönem bulunmamaktadır!");
            //}
            var airportIdsToCheck = new List<int> { form.DepartureAirportId, form.LandingAirportId };
            foreach (var api in airportIdsToCheck)
            {
                var item = _airportRepository.FindBy(x => x.Id == api).FirstOrDefault();
                if (item == null)
                {
                    return ServiceResponse<UmreFormDTO>.Return409("Böyle bir hava alanı bulunmamaktadır!");
                }
            }
            _formRepository.Add(form);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<UmreFormDTO>.Return409("Başvuru işlemi gerçekleşirken bir hata meydana geldi!");
            }
            else
                return ServiceResponse<UmreFormDTO>.ReturnResultWith200(_mapper.Map<UmreFormDTO>(form));
        }
    }
}
