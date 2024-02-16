using AutoMapper;
using ContentManagement.Common.UnitOfWork;
using ContentManagement.Data.Dto;
using ContentManagement.Data.Models;
using ContentManagement.Domain;
using ContentManagement.Helper;
using ContentManagement.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ContentManagement.MediatR.Commands
{
    public class AddMosqueCommandHandler : IRequestHandler<AddMosqueCommand, ServiceResponse<MosqueDTO>>
    {
        private readonly IMosqueRepository repo;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<PTContext> _uow;

        public AddMosqueCommandHandler(IMosqueRepository mosque, IMapper mapper, IUnitOfWork<PTContext> uow)
        {
            repo = mosque;
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<ServiceResponse<MosqueDTO>> Handle(AddMosqueCommand request, CancellationToken cancellationToken)
        {
            var model = _mapper.Map<Mosque>(request);
            repo.Add(model);           
            if (await _uow.SaveAsync()<=0)
            {
                return ServiceResponse<MosqueDTO>.Return409("Ekleme işlemi sırasında bir hata meydana geldi!");
            }
            else
                return ServiceResponse<MosqueDTO>.ReturnResultWith200(_mapper.Map<MosqueDTO>(model));
        }
    }
}
