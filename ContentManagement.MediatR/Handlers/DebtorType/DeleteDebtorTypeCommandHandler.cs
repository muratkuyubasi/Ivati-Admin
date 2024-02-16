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
    public class DeleteDebtorTypeCommandHandler : IRequestHandler<DeleteDebtorTypeCommand, ServiceResponse<DebtorTypeDTO>>
    {
        private readonly IDebtorTypeRepository _debtorTypeRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<PTContext> _uow;

        public DeleteDebtorTypeCommandHandler(IDebtorTypeRepository debtorTypeRepository, IMapper mapper, IUnitOfWork<PTContext> uow)
        {
            _debtorTypeRepository = debtorTypeRepository;
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<ServiceResponse<DebtorTypeDTO>> Handle(DeleteDebtorTypeCommand request, CancellationToken cancellationToken)
        {
            var data = await _debtorTypeRepository.FindBy(x => x.Id == request.Id).FirstOrDefaultAsync();
            if (data == null)
            {
                return ServiceResponse<DebtorTypeDTO>.Return409("Bu ID'ye ait bir havaalanı bulunamadı!");
            }
            _debtorTypeRepository.Remove(data);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<DebtorTypeDTO>.Return409("Silme işlemi sırasında bir hata meydana geldi!");
            }
            else return ServiceResponse<DebtorTypeDTO>.ReturnResultWith200(_mapper.Map<DebtorTypeDTO>(data));
        }
    }
}
