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
    public class AddFoundationPublicationCommandHandler : IRequestHandler<AddFoundationPublicationCommand, ServiceResponse<FoundationPublicationDTO>>
    {
        private readonly IFoundationPublicationRepository repo;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<PTContext> _uow;

        public AddFoundationPublicationCommandHandler(IFoundationPublicationRepository foundationPublicationRepository, IMapper mapper, IUnitOfWork<PTContext> uow)
        {
            repo = foundationPublicationRepository;
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<ServiceResponse<FoundationPublicationDTO>> Handle(AddFoundationPublicationCommand request, CancellationToken cancellationToken)
        {
            var model = _mapper.Map<FoundationPublication>(request);
            repo.Add(model);
            if (await _uow.SaveAsync()<=0)
            {
                return ServiceResponse<FoundationPublicationDTO>.Return409("Ekleme işlemi sırasında bir hata meydana geldi!");
            }
            else
                return ServiceResponse<FoundationPublicationDTO>.ReturnResultWith200(_mapper.Map<FoundationPublicationDTO>(model));
        }
    }
}
