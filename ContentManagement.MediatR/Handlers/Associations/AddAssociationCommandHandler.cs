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
    public class AddAssociationCommandHandler : IRequestHandler<AddAssociationCommand, ServiceResponse<AssociationDTO>>
    {
        private readonly IAssociationRepository _associationRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<PTContext> _uow;

        public AddAssociationCommandHandler(IAssociationRepository associationRepository, IMapper mapper, IUnitOfWork<PTContext> uow)
        {
            _associationRepository = associationRepository;
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<ServiceResponse<AssociationDTO>> Handle(AddAssociationCommand request, CancellationToken cancellationToken)
        {
            var model = _mapper.Map<Association>(request);
            if (!String.IsNullOrWhiteSpace(model.Name))
            {
                _associationRepository.Add(model);
            }
            else return ServiceResponse<AssociationDTO>.Return409("Dernek adı boş bırakılamaz!");
            
            if (await _uow.SaveAsync()<=0)
            {
                return ServiceResponse<AssociationDTO>.Return409("Ekleme işlemi sırasında bir hata meydana geldi!");
            }
            else
                return ServiceResponse<AssociationDTO>.ReturnResultWith200(_mapper.Map<AssociationDTO>(model));
        }
    }
}
