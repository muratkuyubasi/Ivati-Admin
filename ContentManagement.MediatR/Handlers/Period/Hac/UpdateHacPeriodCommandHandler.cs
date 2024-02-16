using AutoMapper;
using ContentManagement.Common.UnitOfWork;
using ContentManagement.Data.Dto;
using ContentManagement.Domain;
using ContentManagement.Helper;
using ContentManagement.MediatR.Commands;
using ContentManagement.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ContentManagement.MediatR.Handlers
{
    public class UpdateHacPeriodCommandHandler : IRequestHandler<UpdateHacPeriodCommand, ServiceResponse<HacPeriodDTO>>
    {
        private readonly IHacPeriodRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<PTContext> _uow;

        public UpdateHacPeriodCommandHandler(IHacPeriodRepository hacPeriodRepository, IMapper mapper, IUnitOfWork<PTContext> unitOfWork)
        {
            _repository = hacPeriodRepository;
            _mapper = mapper;
            _uow = unitOfWork;
        }
        public async Task<ServiceResponse<HacPeriodDTO>> Handle(UpdateHacPeriodCommand request, CancellationToken cancellationToken)
        {
            var data = await _repository.FindBy(x => x.Id == request.Id).FirstOrDefaultAsync();
            if (data == null)
            {
                return ServiceResponse<HacPeriodDTO>.Return404("Bu ID'ye ait bir dönem bulunamadı!");
            }
            data.Name = request.Name;
            data.IsActive = request.IsActive;
            data.StartDate = request.StartDate;
            data.FinishDate = request.FinishDate;
            _repository.Update(data);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<HacPeriodDTO>.Return409("Güncelleme işlemi sırasında bir hata meydana geldi!");
            }
            else return ServiceResponse<HacPeriodDTO>.ReturnResultWith200(_mapper.Map<HacPeriodDTO>(data));
        }
    }
}
