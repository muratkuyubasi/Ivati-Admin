using AutoMapper;
using ContentManagement.Data.Dto;
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
    public class DeleteTranslationCommandHandler : IRequestHandler<DeleteTranslationCommand, ServiceResponse<TranslationDTO>>
    {
        private readonly ITranslationRepository _repo;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<PTContext> _uow;

        public DeleteTranslationCommandHandler(ITranslationRepository translationRepository, IMapper mapper, IUnitOfWork<PTContext> uow)
        {
            _repo = translationRepository;
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<ServiceResponse<TranslationDTO>> Handle(DeleteTranslationCommand request, CancellationToken cancellationToken)
        {
            var data = await _repo.FindBy(x => x.Id == request.Id).FirstOrDefaultAsync();
            if (data == null)
            {
                return ServiceResponse<TranslationDTO>.Return409("Bu ID'ye ait bir çeviri bulunamadı!");
            }
            _repo.Remove(data);
            if (await _uow.SaveAsync()<=0)
            {
                return ServiceResponse<TranslationDTO>.Return409("Silme işlemi sırasında bir hata meydana geldi!");
            }
            else return ServiceResponse<TranslationDTO>.ReturnResultWith200(_mapper.Map<TranslationDTO>(data));
        }
    }
}
