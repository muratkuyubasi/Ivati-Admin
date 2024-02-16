using AutoMapper;
using ContentManagement.Data.Dto;
using ContentManagement.Common.UnitOfWork;
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
    public class UpdateLanguageCommandHandler : IRequestHandler<UpdateLanguageCommand, ServiceResponse<LanguageDTO>>
    {
        private readonly ILanguageRepository _repo;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<PTContext> _uow;

        public UpdateLanguageCommandHandler(ILanguageRepository languageRepository, IMapper mapper, IUnitOfWork<PTContext> uow)
        {
            _repo = languageRepository;
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<ServiceResponse<LanguageDTO>> Handle(UpdateLanguageCommand request, CancellationToken cancellationToken)
        {
            var model = await _repo.FindBy(x => x.Id == request.Id).FirstOrDefaultAsync();
            if (model == null)
            {
                return ServiceResponse<LanguageDTO>.Return409("Bu ID'ye ait bir dil bulunmamaktadır!");
            }
            model.Name = request.Name;
            model.Langcode = request.Langcode;
            model.Flag = request.Flag;
            _repo.Update(model);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<LanguageDTO>.Return409("Güncelleme işlemi sırasında bir hata oluştu");
            }
            else
                return ServiceResponse<LanguageDTO>.ReturnResultWith200(_mapper.Map<LanguageDTO>(model));
        }
    }
}
