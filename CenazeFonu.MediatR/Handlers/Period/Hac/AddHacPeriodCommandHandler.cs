using AutoMapper;
using CenazeFonu.Common.UnitOfWork;
using CenazeFonu.Data;
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
    public class AddHacPeriodCommandHandler : IRequestHandler<AddHacPeriodCommand, ServiceResponse<HacPeriodDTO>>
    {
        private readonly IHacPeriodRepository _hacPeriodRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<PTContext> _uow;
        public AddHacPeriodCommandHandler(IHacPeriodRepository hacPeriodRepository, IMapper mapper, IUnitOfWork<PTContext> uow)
        {
            _hacPeriodRepository = hacPeriodRepository;
            _mapper = mapper;
            _uow = uow;
        }
        public async Task<ServiceResponse<HacPeriodDTO>> Handle(AddHacPeriodCommand request, CancellationToken cancellationToken)
        {
            var data = _hacPeriodRepository.All.Where(x=>x.IsActive == true).FirstOrDefault();
            if (data != null)
            {
                data.IsActive = false;
                _hacPeriodRepository.Update(data);
            }
            var newData = _mapper.Map<HacPeriod>(request);
            if (String.IsNullOrEmpty(newData.Name))
            {
                return ServiceResponse<HacPeriodDTO>.Return409("İsim alanı boş kalamaz!");
            }
            newData.StartDate = request.StartDate;
            newData.FinishDate = request.FinishDate;
            _hacPeriodRepository.Add(newData);
            if (await _uow.SaveAsync() < 0)
            {
                return ServiceResponse<HacPeriodDTO>.Return409("Ekleme işlemi sırasında bir hata meydana geldi!");
            }
            else return ServiceResponse<HacPeriodDTO>.ReturnResultWith200(_mapper.Map<HacPeriodDTO>(newData));

        }
    }
}
