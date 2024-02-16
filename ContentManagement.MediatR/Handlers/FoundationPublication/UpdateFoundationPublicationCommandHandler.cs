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
    public class UpdateFoundationPublicationCommandHandler : IRequestHandler<UpdateFoundationPublicationCommand, ServiceResponse<FoundationPublicationDTO>>
    {
        private readonly IFoundationPublicationRepository repo;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<PTContext> _uow;

        public UpdateFoundationPublicationCommandHandler(IFoundationPublicationRepository foundationPublicationRepository, IMapper mapper, IUnitOfWork<PTContext> uow)
        {
            repo = foundationPublicationRepository;
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<ServiceResponse<FoundationPublicationDTO>> Handle(UpdateFoundationPublicationCommand request, CancellationToken cancellationToken)
        {
            var model = await repo.FindBy(x => x.Id == request.Id).FirstOrDefaultAsync();
            if (model == null)
            {
                return ServiceResponse<FoundationPublicationDTO>.Return409("Bu ID'ye ait bir vakıf yayını bulunmamaktadır!");
            }
            model.Image = request.Image;
            model.Name = request.Name;
            model.Subtitle = request.Subtitle;
            model.Description = request.Description;
            model.CompiledBy = request.CompiledBy;
            model.Translator = request.Translator;
            model.RequestNumber = request.RequestNumber;
            model.PublicationDate = request.PublicationDate;
            repo.Update(model);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<FoundationPublicationDTO>.Return409("Güncelleme işlemi sırasında bir hata oluştu");
            }
            else
                return ServiceResponse<FoundationPublicationDTO>.ReturnResultWith200(_mapper.Map<FoundationPublicationDTO>(model));
        }
    }
}
