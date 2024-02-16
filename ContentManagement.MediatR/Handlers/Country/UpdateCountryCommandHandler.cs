using AutoMapper;
using ContentManagement.Common.UnitOfWork;
using ContentManagement.Data.Dto;
using ContentManagement.Data.Models;
using ContentManagement.Domain;
using ContentManagement.Domain.Migrations;
using ContentManagement.Helper;
using ContentManagement.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ContentManagement.MediatR.Commands
{
    public class UpdateCountryCommandHandler : IRequestHandler<UpdateCountryCommand, ServiceResponse<CountryDTO>>
    {
        private readonly ICountryRepository _repo;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<PTContext> _uow;

        public UpdateCountryCommandHandler(ICountryRepository countryRepository, IMapper mapper, IUnitOfWork<PTContext> uow)
        {
            _repo = countryRepository;
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<ServiceResponse<CountryDTO>> Handle(UpdateCountryCommand request, CancellationToken cancellationToken)
        {
            var model = await _repo.FindBy(x => x.Id == request.Id).FirstOrDefaultAsync();
            if (model == null) {         
                return ServiceResponse<CountryDTO>.Return409("Bu ID'ye ait bir ülke bulunmamaktadır!");
            }
            model.Name = request.Name;
            model.Langcode = request.Langcode;
            _repo.Update(model);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<CountryDTO>.Return409("Güncelleme işlemi sırasında bir hata oluştu");
            }
            else
                return ServiceResponse<CountryDTO>.ReturnResultWith200(_mapper.Map<CountryDTO>(model));
        }
    }
}
