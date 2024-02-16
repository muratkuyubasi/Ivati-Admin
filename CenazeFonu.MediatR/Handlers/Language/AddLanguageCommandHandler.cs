using AutoMapper;
using CenazeFonu.Data;
using CenazeFonu.Data.Dto;
using CenazeFonu.Common.UnitOfWork;
using CenazeFonu.Data.Models;
using CenazeFonu.Domain;
using CenazeFonu.Helper;
using CenazeFonu.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CenazeFonu.MediatR.Commands
{
    public class AddLanguageCommandHandler : IRequestHandler<AddLanguageCommand, ServiceResponse<LanguageDTO>>
    {
        private readonly ILanguageRepository _repo;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<PTContext> _uow;

        public AddLanguageCommandHandler(ILanguageRepository languageRepository, IMapper mapper, IUnitOfWork<PTContext> uow)
        {
            _repo = languageRepository;
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<ServiceResponse<LanguageDTO>> Handle(AddLanguageCommand request, CancellationToken cancellationToken)
        {
            var model = _mapper.Map<Language>(request);        
            _repo.Add(model);
            if (await _uow.SaveAsync()<=0)
            {
                return ServiceResponse<LanguageDTO>.Return409("Ekleme işlemi sırasında bir hata meydana geldi!");
            }
            else
                return ServiceResponse<LanguageDTO>.ReturnResultWith200(_mapper.Map<LanguageDTO>(model));
        }
    }
}
