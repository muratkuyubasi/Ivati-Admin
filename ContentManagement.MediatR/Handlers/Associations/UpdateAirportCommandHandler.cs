using AutoMapper;
using ContentManagement.Common.UnitOfWork;
using ContentManagement.Data.Dto;
using ContentManagement.Data.Models;
using ContentManagement.Domain;
using ContentManagement.Domain.Migrations;
using ContentManagement.Helper;
using ContentManagement.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ContentManagement.MediatR.Commands
{
    public class UpdateAssociationCommandHandler : IRequestHandler<UpdateAssociationCommand, ServiceResponse<AssociationDTO>>
    {
        private readonly IAssociationRepository _associationRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<PTContext> _uow;

        public UpdateAssociationCommandHandler(IAssociationRepository associationRepository, IMapper mapper, IUnitOfWork<PTContext> uow)
        {
            _associationRepository = associationRepository;
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<ServiceResponse<AssociationDTO>> Handle(UpdateAssociationCommand request, CancellationToken cancellationToken)
        {
            var model = await _associationRepository.FindBy(x => x.Id == request.Id).FirstOrDefaultAsync();
            if (model == null)
            {
                return ServiceResponse<AssociationDTO>.Return409("Bu ID'ye ait bir dernek bulunmamaktadır!");
            }
            if (!String.IsNullOrEmpty(request.Name))
            {
                model.Name = request.Name;
            }
            else return ServiceResponse<AssociationDTO>.Return409("Dernek adı boş kalamaz!");

            _associationRepository.Update(model);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<AssociationDTO>.Return409("Güncelleme işlemi sırasında bir hata oluştu");
            }
            else
                return ServiceResponse<AssociationDTO>.ReturnResultWith200(_mapper.Map<AssociationDTO>(model));
        }
    }
}
