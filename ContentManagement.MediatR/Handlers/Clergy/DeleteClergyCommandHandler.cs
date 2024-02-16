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
    public class DeleteClergyCommandHandler : IRequestHandler<DeleteClergyCommand, ServiceResponse<ClergyDTO>>
    {
        private readonly IClergyRepository repo;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<PTContext> _uow;

        public DeleteClergyCommandHandler(IClergyRepository clergyRepository, IMapper mapper, IUnitOfWork<PTContext> uow)
        {
            repo = clergyRepository;
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<ServiceResponse<ClergyDTO>> Handle(DeleteClergyCommand request, CancellationToken cancellationToken)
        {
            var data = await repo.FindBy(x => x.Id == request.Id).FirstOrDefaultAsync();
            if (data == null)
            {
                return ServiceResponse<ClergyDTO>.Return409("Bu ID'ye ait bir din görevlisi bulunamadı!");
            }
            repo.Remove(data);
            if (await _uow.SaveAsync()<=0)
            {
                return ServiceResponse<ClergyDTO>.Return409("Silme işlemi sırasında bir hata meydana geldi!");
            }
            else return ServiceResponse<ClergyDTO>.ReturnResultWith200(_mapper.Map<ClergyDTO>(data));
        }
    }
}
