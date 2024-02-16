using AutoMapper;
using CenazeFonu.Common.UnitOfWork;
using CenazeFonu.Data.Dto;
using CenazeFonu.Domain;
using CenazeFonu.Helper;
using CenazeFonu.MediatR.Commands;
using CenazeFonu.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CenazeFonu.MediatR.Handlers
{
    public class DeleteServicesCommandHandler : IRequestHandler<DeleteServicesCommand, ServiceResponse<ServicesDTO>>
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<PTContext> _uow;

        public DeleteServicesCommandHandler(IServiceRepository serviceRepository, IMapper mapper, IUnitOfWork<PTContext> uow)
        {
            _serviceRepository = serviceRepository;
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<ServiceResponse<ServicesDTO>> Handle(DeleteServicesCommand request, CancellationToken cancellationToken)
        {
            var data = await _serviceRepository.FindBy(x => x.Id == request.Id).FirstOrDefaultAsync();
            if (data == null) { return ServiceResponse<ServicesDTO>.Return409("Bu ID'ye ait bir hizmet bulunamadı!"); }
            _serviceRepository.Remove(data);
            if (await _uow.SaveAsync() <= 0) { return ServiceResponse<ServicesDTO>.Return409("Silme işlemi sırasında bir hata meydana geldi!"); }
            else return ServiceResponse<ServicesDTO>.ReturnResultWith200(_mapper.Map<ServicesDTO>(data));
        }
    }
}
