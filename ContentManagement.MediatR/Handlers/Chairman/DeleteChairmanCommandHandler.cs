using AutoMapper;
using ContentManagement.Data;
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
    public class DeleteChairmanCommandHandler : IRequestHandler<DeleteChairmanCommand, ServiceResponse<ChairmanDTO>>
    {
        private readonly IChairmanRepository repo;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<PTContext> _uow;

        public DeleteChairmanCommandHandler(IChairmanRepository chairmanRepository, IMapper mapper, IUnitOfWork<PTContext> uow)
        {
            repo = chairmanRepository;
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<ServiceResponse<ChairmanDTO>> Handle(DeleteChairmanCommand request, CancellationToken cancellationToken)
        {
            var data = await repo.FindBy(x => x.Id == request.Id).FirstOrDefaultAsync();
            if (data == null)
            {
                return ServiceResponse<ChairmanDTO>.Return409("Bu ID'ye ait bir yönetim kurulu başkanı bulunamadı!");
            }
            repo.Remove(data);
            if (await _uow.SaveAsync()<=0)
            {
                return ServiceResponse<ChairmanDTO>.Return409("Silme işlemi sırasında bir hata meydana geldi!");
            }
            else return ServiceResponse<ChairmanDTO>.ReturnResultWith200(_mapper.Map<ChairmanDTO>(data));
        }
    }
}
