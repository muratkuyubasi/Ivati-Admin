using AutoMapper;
using ContentManagement.Data.Dto;
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
    public class DeleteAtaseCommandHandler : IRequestHandler<DeleteAtaseCommand, ServiceResponse<AtaseModelDTO>>
    {
        private readonly IAtaseRepository repo;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<PTContext> _uow;

        public DeleteAtaseCommandHandler(IAtaseRepository ataseRepository, IMapper mapper, IUnitOfWork<PTContext> uow)
        {
            repo = ataseRepository;
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<ServiceResponse<AtaseModelDTO>> Handle(DeleteAtaseCommand request, CancellationToken cancellationToken)
        {
            var data = await repo.FindBy(x => x.Id == request.Id).FirstOrDefaultAsync();
            if (data == null)
            {
                return ServiceResponse<AtaseModelDTO>.Return409("Bu ID'ye ait bir ateşe bulunamadı!");
            }
            repo.Remove(data);
            if (await _uow.SaveAsync()<=0)
            {
                return ServiceResponse<AtaseModelDTO>.Return409("Silme işlemi sırasında bir hata meydana geldi!");
            }
            else return ServiceResponse<AtaseModelDTO>.ReturnResultWith200(_mapper.Map<AtaseModelDTO>(data));
        }
    }
}
