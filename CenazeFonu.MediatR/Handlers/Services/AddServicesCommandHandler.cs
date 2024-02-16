using AutoMapper;
using CenazeFonu.Common.UnitOfWork;
using CenazeFonu.Data.Dto;
using CenazeFonu.Data.Models;
using CenazeFonu.Domain;
using CenazeFonu.Helper;
using CenazeFonu.MediatR.Commands;
using CenazeFonu.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CenazeFonu.MediatR.Handlers
{
    public class AddServicesCommandHandler : IRequestHandler<AddServicesCommand, ServiceResponse<ServicesDTO>>
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<PTContext> _uow;

        public AddServicesCommandHandler(IServiceRepository serviceRepository, IMapper mapper, IUnitOfWork<PTContext> uow)
        {
            _serviceRepository = serviceRepository;
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<ServiceResponse<ServicesDTO>> Handle(AddServicesCommand request, CancellationToken cancellationToken)
        {
            var data = _mapper.Map<Services>(request);
            data.CreationDate = DateTime.Now;
            _serviceRepository.Add(data);
            if (await _uow.SaveAsync() <= 0) { return ServiceResponse<ServicesDTO>.Return409("Ekleme işlemi sırasında bir hata meydana geldi!"); }
            else return ServiceResponse<ServicesDTO>.ReturnResultWith200(_mapper.Map<ServicesDTO>(data));
        }
    }
}
