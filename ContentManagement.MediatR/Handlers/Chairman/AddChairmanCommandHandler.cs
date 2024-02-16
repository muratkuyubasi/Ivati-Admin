using AutoMapper;
using ContentManagement.Data;
using ContentManagement.Data.Dto;
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
    public class AddChairmanCommandHandler : IRequestHandler<AddChairmanCommand, ServiceResponse<ChairmanDTO>>
    {
        private readonly IChairmanRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<PTContext> _uow;

        public AddChairmanCommandHandler(IChairmanRepository chairmanRepository, IMapper mapper, IUnitOfWork<PTContext> uow)
        {
            _repository = chairmanRepository;
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<ServiceResponse<ChairmanDTO>> Handle(AddChairmanCommand request, CancellationToken cancellationToken)
        {
            var model = _mapper.Map<Chairman>(request);
            _repository.Add(model);
            if (await _uow.SaveAsync()<=0)
            {
                return ServiceResponse<ChairmanDTO>.Return409("Ekleme işlemi sırasında bir hata meydana geldi!");
            }
            else
                return ServiceResponse<ChairmanDTO>.ReturnResultWith200(_mapper.Map<ChairmanDTO>(model));
        }
    }
}
