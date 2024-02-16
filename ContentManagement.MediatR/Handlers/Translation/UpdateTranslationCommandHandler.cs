using AutoMapper;
using ContentManagement.Data.Dto;
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
    public class UpdateTranslationCommandHandler : IRequestHandler<UpdateTranslationCommand, ServiceResponse<TranslationDTO>>
    {
        private readonly ITranslationRepository _repo;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<PTContext> _uow;

        public UpdateTranslationCommandHandler(ITranslationRepository translationRepository, IMapper mapper, IUnitOfWork<PTContext> uow)
        {
            _repo = translationRepository;
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<ServiceResponse<TranslationDTO>> Handle(UpdateTranslationCommand request, CancellationToken cancellationToken)
        {
            var model = await _repo.FindBy(x => x.Id == request.Id).FirstOrDefaultAsync();
            if (model == null)
            {
                return ServiceResponse<TranslationDTO>.Return409("Bu ID'ye ait bir çeviri bulunmamaktadır!");
            }
            model.Title = request.Title;
            model.Description = request.Description;
            model.Author = request.Author;
            model.LanguageId = request.LanguageId;
            model.Image = request.Image;
            model.LinkImage = request.LinkImage;
            model.LinkUrl = request.LinkUrl;
            model.Order = request.Order;
            _repo.Update(model);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<TranslationDTO>.Return409("Güncelleme işlemi sırasında bir hata oluştu");
            }
            else
                return ServiceResponse<TranslationDTO>.ReturnResultWith200(_mapper.Map<TranslationDTO>(model));
        }
    }
}
