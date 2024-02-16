using AutoMapper;
using CenazeFonu.Common.UnitOfWork;
using CenazeFonu.Data.Dto;
using CenazeFonu.Domain;
using CenazeFonu.Helper;
using CenazeFonu.MediatR.Commands;
using CenazeFonu.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CenazeFonu.MediatR.Handlers
{
    public class DeleteMosqueCommandHandler : IRequestHandler<DeleteMosqueCommand, ServiceResponse<MosqueDTO>>
    {
        private readonly IMosqueRepository repo;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<PTContext> _uow;

        public DeleteMosqueCommandHandler(IMosqueRepository mosqueRepository, IMapper mapper, IUnitOfWork<PTContext> uow)
        {
            repo = mosqueRepository;
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<ServiceResponse<MosqueDTO>> Handle(DeleteMosqueCommand request, CancellationToken cancellationToken)
        {
            var data = await repo.FindBy(x => x.Id == request.Id).FirstOrDefaultAsync();
            if (data == null)
            {
                return ServiceResponse<MosqueDTO>.Return409("Bu ID'ye ait bir cami bulunamadı!");
            }
            repo.Remove(data);
            if (await _uow.SaveAsync()<=0)
            {
                return ServiceResponse<MosqueDTO>.Return409("Silme işlemi sırasında bir hata meydana geldi!");
            }
            else return ServiceResponse<MosqueDTO>.ReturnResultWith200(_mapper.Map<MosqueDTO>(data));
        }
    }
}
