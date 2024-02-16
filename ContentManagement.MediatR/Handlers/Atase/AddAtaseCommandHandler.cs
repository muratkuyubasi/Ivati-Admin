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
    public class AddAtaseCommandHandler : IRequestHandler<AddAtaseCommand, ServiceResponse<AtaseModelDTO>>
    {
        private readonly IAtaseRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<PTContext> _uow;

        public AddAtaseCommandHandler(IAtaseRepository ataseRepository, IMapper mapper, IUnitOfWork<PTContext> uow)
        {
            _repository = ataseRepository;
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<ServiceResponse<AtaseModelDTO>> Handle(AddAtaseCommand request, CancellationToken cancellationToken)
        {
            var model = _mapper.Map<AtaseModel>(request);
            _repository.Add(model);
            if (await _uow.SaveAsync()<=0)
            {
                return ServiceResponse<AtaseModelDTO>.Return409("Ekleme işlemi sırasında bir hata meydana geldi!");
            }
            else
                return ServiceResponse<AtaseModelDTO>.ReturnResultWith200(_mapper.Map<AtaseModelDTO>(model));
        }
    }
}
