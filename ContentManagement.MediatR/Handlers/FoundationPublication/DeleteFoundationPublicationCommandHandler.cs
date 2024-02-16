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
    public class DeleteFoundationPublicationCommandHandler : IRequestHandler<DeleteFoundationPublicationCommand, ServiceResponse<FoundationPublicationDTO>>
    {
        private readonly IFoundationPublicationRepository repo;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<PTContext> _uow;

        public DeleteFoundationPublicationCommandHandler(IFoundationPublicationRepository foundationPublicationRepository, IMapper mapper, IUnitOfWork<PTContext> uow)
        {
            repo = foundationPublicationRepository;
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<ServiceResponse<FoundationPublicationDTO>> Handle(DeleteFoundationPublicationCommand request, CancellationToken cancellationToken)
        {
            var data = await repo.FindBy(x => x.Id == request.Id).FirstOrDefaultAsync();
            if (data == null)
            {
                return ServiceResponse<FoundationPublicationDTO>.Return409("Bu ID'ye ait bir vakıf yayını bulunamadı!");
            }
            repo.Remove(data);
            if (await _uow.SaveAsync()<=0)
            {
                return ServiceResponse<FoundationPublicationDTO>.Return409("Silme işlemi sırasında bir hata meydana geldi!");
            }
            else return ServiceResponse<FoundationPublicationDTO>.ReturnResultWith200(_mapper.Map<FoundationPublicationDTO>(data));
        }
    }
}
