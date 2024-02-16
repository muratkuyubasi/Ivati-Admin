using AutoMapper;
using CenazeFonu.Common.UnitOfWork;
using CenazeFonu.Data.Dto;
using CenazeFonu.Data.Models;
using CenazeFonu.Domain;
using CenazeFonu.Helper;
using CenazeFonu.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CenazeFonu.MediatR.Commands
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
