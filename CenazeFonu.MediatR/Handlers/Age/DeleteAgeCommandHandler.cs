using AutoMapper;
using CenazeFonu.Common.UnitOfWork;
using CenazeFonu.Data.Dto;
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
    public class DeleteAgeCommandHandler : IRequestHandler<DeleteAgeCommand, ServiceResponse<AgeDTO>>
    {
        private readonly IAgeRepository _ageRepository;
        private readonly IUnitOfWork<PTContext> _uow;
        private readonly IMapper _mapper;

        public DeleteAgeCommandHandler(IAgeRepository ageRepository, IUnitOfWork<PTContext> uow, IMapper mapper)
        {
            _ageRepository = ageRepository;
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<AgeDTO>> Handle(DeleteAgeCommand request, CancellationToken cancellationToken)
        {
            var age = _ageRepository.FindBy(x=>x.Id == request.Id).FirstOrDefault();
            if (age == null)
            {
                return ServiceResponse<AgeDTO>.Return409("Bu ID'ye ait bir yaş aralığı bulunamadı!");
            }
            _ageRepository.Remove(age);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<AgeDTO>.Return409("Silme işlemi sırasında bir hata meydana geldi!");
            }
            else return ServiceResponse<AgeDTO>.ReturnResultWith200(_mapper.Map<AgeDTO>(age));
        }
    }
}
