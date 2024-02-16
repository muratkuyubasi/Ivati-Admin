using AutoMapper;
using ContentManagement.Common.UnitOfWork;
using ContentManagement.Data.Dto;
using ContentManagement.Domain;
using ContentManagement.Helper;
using ContentManagement.MediatR.Commands;
using ContentManagement.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace ContentManagement.MediatR.Handlers
{
    public class UpdateUmrePeriodCommandHandler : IRequestHandler<UpdateUmrePeriodCommand, ServiceResponse<UmrePeriodDTO>>
    {
        private readonly IUmrePeriodRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<PTContext> _uow;

        public UpdateUmrePeriodCommandHandler(IUmrePeriodRepository hacPeriodRepository, IMapper mapper, IUnitOfWork<PTContext> unitOfWork)
        {
            _repository = hacPeriodRepository;
            _mapper = mapper;
            _uow = unitOfWork;
        }
        public async Task<ServiceResponse<UmrePeriodDTO>> Handle(UpdateUmrePeriodCommand request, CancellationToken cancellationToken)
        {
            var data = await _repository.FindBy(x => x.Id == request.Id).FirstOrDefaultAsync();
            if (data == null)
            {
                return ServiceResponse<UmrePeriodDTO>.Return404("Bu ID'ye ait bir dönem bulunamadı!");
            }
            data.Name = request.Name;
            data.IsActive = request.IsActive;
            data.StartDate = request.StartDate;
            data.FinishDate = request.FinishDate;
            _repository.Update(data);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<UmrePeriodDTO>.Return409("Güncelleme işlemi sırasında bir hata meydana geldi!");
            }
            else return ServiceResponse<UmrePeriodDTO>.ReturnResultWith200(_mapper.Map<UmrePeriodDTO>(data));
        }
    }
}
