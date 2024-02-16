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
    public class DeleteExecutiveCommandHandler : IRequestHandler<DeleteExecutiveCommand, ServiceResponse<ExecutiveDTO>>
    {
        private readonly IExecutiveRepository _executiveRepository;
        private readonly IUnitOfWork<PTContext> _uow;
        private readonly IMapper _mapper;

        public DeleteExecutiveCommandHandler(IExecutiveRepository executiveRepository, IUnitOfWork<PTContext> uow, IMapper mapper)
        {
            _executiveRepository = executiveRepository;
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<ExecutiveDTO>> Handle(DeleteExecutiveCommand request, CancellationToken cancellationToken)
        {
            var data = _executiveRepository.FindBy(X => X.Id == request.Id).FirstOrDefault();
            if (data == null)
            {
                return ServiceResponse<ExecutiveDTO>.Return404("Bu ID'ye ait bir yönetici kaydı bulunmamaktadır!");
            }
            _executiveRepository.Remove(data);
            if (await _uow.SaveAsync()<0)
            {
                return ServiceResponse<ExecutiveDTO>.Return409("Silme işlemi sırasında bir hata meydana geldi!");
            }
            return ServiceResponse<ExecutiveDTO>.ReturnResultWith200(_mapper.Map<ExecutiveDTO>(data));
        }
    }
}
