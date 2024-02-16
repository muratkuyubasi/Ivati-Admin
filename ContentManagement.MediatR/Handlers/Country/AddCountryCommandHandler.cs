using AutoMapper;
using ContentManagement.Common.UnitOfWork;
using ContentManagement.Data;
using ContentManagement.Data.Dto;
using ContentManagement.Data.Models;
using ContentManagement.Domain;
using ContentManagement.Helper;
using ContentManagement.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ContentManagement.MediatR.Commands
{
    public class AddCountryCommandHandler : IRequestHandler<AddCountryCommand, ServiceResponse<CountryDTO>>
    {
        private readonly ICountryRepository _repo;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<PTContext> _uow;

        public AddCountryCommandHandler(ICountryRepository countryRepository, IMapper mapper, IUnitOfWork<PTContext> uow)
        {
            _repo = countryRepository;
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<ServiceResponse<CountryDTO>> Handle(AddCountryCommand request, CancellationToken cancellationToken)
        {
            var model = _mapper.Map<Country>(request);       
            _repo.Add(model);
            if (await _uow.SaveAsync()<=0)
            {
                return ServiceResponse<CountryDTO>.Return409("Ekleme işlemi sırasında bir hata meydana geldi!");
            }
            else
                return ServiceResponse<CountryDTO>.ReturnResultWith200(_mapper.Map<CountryDTO>(model));
        }
    }
}
