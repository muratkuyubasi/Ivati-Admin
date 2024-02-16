﻿using AutoMapper;
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
    public class AddProjectCommandHandler : IRequestHandler<AddProjectCommand, ServiceResponse<ProjectDTO>>
    {
        private readonly IProjectRepository _repo;
        private readonly ILanguageRepository _languageRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<PTContext> _uow;

        public AddProjectCommandHandler(IProjectRepository translationRepository, IMapper mapper, IUnitOfWork<PTContext> uow, ILanguageRepository languageRepository)
        {
            _repo = translationRepository;
            _mapper = mapper;
            _uow = uow;
            _languageRepository = languageRepository;
        }

        public async Task<ServiceResponse<ProjectDTO>> Handle(AddProjectCommand request, CancellationToken cancellationToken)
        {
            var model = _mapper.Map<Project>(request);
            var languageExists = _languageRepository.FindBy(X => X.Id == request.LanguageId).FirstOrDefault();
            if (languageExists == null)
            {
                return ServiceResponse<ProjectDTO>.Return409("Bu ID'ye ait bir dil bulunmamaktadır!");
            }
            _repo.Add(model);
            if (await _uow.SaveAsync()<=0)
            {
                return ServiceResponse<ProjectDTO>.Return409("Ekleme işlemi sırasında bir hata meydana geldi!");
            }
            else
                return ServiceResponse<ProjectDTO>.ReturnResultWith200(_mapper.Map<ProjectDTO>(model));
        }
    }
}