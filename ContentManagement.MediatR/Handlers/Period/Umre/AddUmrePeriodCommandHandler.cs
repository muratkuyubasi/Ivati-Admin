using AutoMapper;
using ContentManagement.Common.UnitOfWork;
using ContentManagement.Data;
using ContentManagement.Data.Dto;
using ContentManagement.Domain;
using ContentManagement.Helper;
using ContentManagement.MediatR.Commands;
using ContentManagement.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ContentManagement.MediatR.Handlers
{
    public class AddUmrePeriodCommandHandler : IRequestHandler<AddUmrePeriodCommand, ServiceResponse<UmrePeriodDTO>>
    {
        private readonly IUmrePeriodRepository _umrePeriodRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<PTContext> _uow;
        public AddUmrePeriodCommandHandler(IUmrePeriodRepository umrePeriodRepository, IMapper mapper, IUnitOfWork<PTContext> uow)
        {
            _umrePeriodRepository = umrePeriodRepository;
            _mapper = mapper;
            _uow = uow;
        }
        public async Task<ServiceResponse<UmrePeriodDTO>> Handle(AddUmrePeriodCommand request, CancellationToken cancellationToken)
        {
            var data = _umrePeriodRepository.All.Where(x=>x.IsActive == true).FirstOrDefault();
            if (data != null)
            {
                data.IsActive = false;
                _umrePeriodRepository.Update(data);
            }
            var newData = _mapper.Map<UmrePeriod>(request);
            if (String.IsNullOrEmpty(newData.Name))
            {
                return ServiceResponse<UmrePeriodDTO>.Return409("İsim alanı boş kalamaz!");
            }
            newData.StartDate = request.StartDate;
            newData.FinishDate = request.FinishDate;
            _umrePeriodRepository.Add(newData);
            if (await _uow.SaveAsync() < 0)
            {
                return ServiceResponse<UmrePeriodDTO>.Return409("Ekleme işlemi sırasında bir hata meydana geldi!");
            }
            else return ServiceResponse<UmrePeriodDTO>.ReturnResultWith200(_mapper.Map<UmrePeriodDTO>(newData));

        }
    }
}
