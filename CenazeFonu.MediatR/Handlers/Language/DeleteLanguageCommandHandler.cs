using AutoMapper;
using CenazeFonu.Data.Dto;
using CenazeFonu.Common.UnitOfWork;
using CenazeFonu.Domain;
using CenazeFonu.Helper;
using CenazeFonu.MediatR.Commands;
using CenazeFonu.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CenazeFonu.MediatR.Handlers
{
    public class DeleteLanguageCommandHandler : IRequestHandler<DeleteLanguageCommand, ServiceResponse<LanguageDTO>>
    {
        private readonly ILanguageRepository _repo;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<PTContext> _uow;

        public DeleteLanguageCommandHandler(ILanguageRepository languageRepository, IMapper mapper, IUnitOfWork<PTContext> uow)
        {
            _repo = languageRepository;
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<ServiceResponse<LanguageDTO>> Handle(DeleteLanguageCommand request, CancellationToken cancellationToken)
        {
            var data = await _repo.FindBy(x => x.Id == request.Id).FirstOrDefaultAsync();
            if (data == null)
            {
                return ServiceResponse<LanguageDTO>.Return409("Bu ID'ye ait dil bulunamadı!");
            }
            _repo.Remove(data);
            if (await _uow.SaveAsync()<=0)
            {
                return ServiceResponse<LanguageDTO>.Return409("Silme işlemi sırasında bir hata meydana geldi!");
            }
            else return ServiceResponse<LanguageDTO>.ReturnResultWith200(_mapper.Map<LanguageDTO>(data));
        }
    }
}
