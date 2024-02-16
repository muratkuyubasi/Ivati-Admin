using AutoMapper;
using ContentManagement.Common.UnitOfWork;
using ContentManagement.Data.Dto;
using ContentManagement.Data.Models;
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
    public class AddDebtorTypeCommandHandler : IRequestHandler<AddDebtorTypeCommand, ServiceResponse<DebtorTypeDTO>>
    {
        private readonly IDebtorTypeRepository _debtorTypeRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<PTContext> _uow;

        public AddDebtorTypeCommandHandler(IDebtorTypeRepository debtorTypeRepository, IMapper mapper, IUnitOfWork<PTContext> uow)
        {
            _debtorTypeRepository = debtorTypeRepository;
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<ServiceResponse<DebtorTypeDTO>> Handle(AddDebtorTypeCommand request, CancellationToken cancellationToken)
        {
            var model = _mapper.Map<DebtorType>(request);
            if (String.IsNullOrWhiteSpace(model.Name))
            {
                return ServiceResponse<DebtorTypeDTO>.Return409("Ad alanı boş kalamaz!");
            }
            _debtorTypeRepository.Add(model);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<DebtorTypeDTO>.Return409("Ekleme işlemi sırasında bir hata meydana geldi!");
            }
            else return ServiceResponse<DebtorTypeDTO>.ReturnResultWith200(_mapper.Map<DebtorTypeDTO>(model));
        }
    }
}
