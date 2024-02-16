using AutoMapper;
using CenazeFonu.Common.UnitOfWork;
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
    public class AddClergyCommandHandler : IRequestHandler<AddClergyCommand, ServiceResponse<ClergyDTO>>
    {
        private readonly IClergyRepository _clergyRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<PTContext> _uow;

        public AddClergyCommandHandler(IClergyRepository clergyRepository, IMapper mapper, IUnitOfWork<PTContext> uow)
        {
            _clergyRepository = clergyRepository;
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<ServiceResponse<ClergyDTO>> Handle(AddClergyCommand request, CancellationToken cancellationToken)
        {
            var model = _mapper.Map<Clergy>(request);
            _clergyRepository.Add(model);
            if (await _uow.SaveAsync()<=0)
            {
                return ServiceResponse<ClergyDTO>.Return409("Ekleme işlemi sırasında bir hata meydana geldi!");
            }
            else
                return ServiceResponse<ClergyDTO>.ReturnResultWith200(_mapper.Map<ClergyDTO>(model));
        }
    }
}
