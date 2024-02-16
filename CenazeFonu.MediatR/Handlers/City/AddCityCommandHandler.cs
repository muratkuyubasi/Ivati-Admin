using AutoMapper;
using CenazeFonu.Common.UnitOfWork;
using CenazeFonu.Data;
using CenazeFonu.Data.Dto;
using CenazeFonu.Data.Models;
using CenazeFonu.Domain;
using CenazeFonu.Helper;
using CenazeFonu.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CenazeFonu.MediatR.Commands
{
    public class AddCityCommandHandler : IRequestHandler<AddCityCommand, ServiceResponse<CityDTO>>
    {
        private readonly ICityRepository _repo;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<PTContext> _uow;

        public AddCityCommandHandler(ICityRepository cityRepository, IMapper mapper, IUnitOfWork<PTContext> uow)
        {
            _repo = cityRepository;
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<ServiceResponse<CityDTO>> Handle(AddCityCommand request, CancellationToken cancellationToken)
        {
            var model = _mapper.Map<City>(request);        
            _repo.Add(model);
            if (await _uow.SaveAsync()<=0)
            {
                return ServiceResponse<CityDTO>.Return409("Ekleme işlemi sırasında bir hata meydana geldi!");
            }
            else
                return ServiceResponse<CityDTO>.ReturnResultWith200(_mapper.Map<CityDTO>(model));
        }
    }
}
