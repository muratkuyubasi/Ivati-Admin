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
    public class UpdateDebtorTypeCommandHandler : IRequestHandler<UpdateDebtorTypeCommand, ServiceResponse<DebtorTypeDTO>>
    {
        private readonly IDebtorTypeRepository _debtorTypeRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<PTContext> _uow;

        public UpdateDebtorTypeCommandHandler(IDebtorTypeRepository debtorTypeRepository, IMapper mapper, IUnitOfWork<PTContext> uow)
        {
            _debtorTypeRepository = debtorTypeRepository;
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<ServiceResponse<DebtorTypeDTO>> Handle(UpdateDebtorTypeCommand request, CancellationToken cancellationToken)
        {
            var data = _debtorTypeRepository.FindBy(x=>x.Id == request.Id).FirstOrDefault();
            if (data == null)
            {
                return ServiceResponse<DebtorTypeDTO>.Return409("Bu ID'ye ait bir borç tipi bulunmamaktadır!");
            }
            data.Name = request.Name;
            _debtorTypeRepository.Update(data);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<DebtorTypeDTO>.Return409("Güncelleme işlemi sırasında bir hata meydana geldi!");
            }
            else return ServiceResponse<DebtorTypeDTO>.ReturnResultWith200(_mapper.Map<DebtorTypeDTO>(data));
        }
    }
}
