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
    public class DeleteAssociationCommandHandler : IRequestHandler<DeleteAssociationCommand, ServiceResponse<AssociationDTO>>
    {
        private readonly IAssociationRepository _associationRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<PTContext> _uow;

        public DeleteAssociationCommandHandler(IAssociationRepository associationRepository, IMapper mapper, IUnitOfWork<PTContext> uow)
        {
            _associationRepository = associationRepository;
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<ServiceResponse<AssociationDTO>> Handle(DeleteAssociationCommand request, CancellationToken cancellationToken)
        {
            var data = await _associationRepository.FindBy(x => x.Id == request.Id).FirstOrDefaultAsync();
            if (data == null)
            {
                return ServiceResponse<AssociationDTO>.Return409("Bu ID'ye ait bir dernek bulunamadı!");
            }
            _associationRepository.Remove(data);
            if (await _uow.SaveAsync()<=0)
            {
                return ServiceResponse<AssociationDTO>.Return409("Silme işlemi sırasında bir hata meydana geldi!");
            }
            else return ServiceResponse<AssociationDTO>.ReturnResultWith200(_mapper.Map<AssociationDTO>(data));
        }
    }
}
