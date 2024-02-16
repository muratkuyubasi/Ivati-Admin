using AutoMapper;
using ContentManagement.Data.Dto;
using ContentManagement.Helper;
using ContentManagement.MediatR.Queries;
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
    public class GetFoundationPublicationByIdQueryHandler : IRequestHandler<GetFoundationPublicationByIdQuery, ServiceResponse<FoundationPublicationDTO>>
    {
        private readonly IFoundationPublicationRepository repository;
        private readonly IMapper _mapper;

        public GetFoundationPublicationByIdQueryHandler(IFoundationPublicationRepository repo, IMapper mapper)
        {
            repository = repo;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<FoundationPublicationDTO>> Handle(GetFoundationPublicationByIdQuery request, CancellationToken cancellationToken)
        {
            var data = await repository.FindBy(x => x.Id == request.Id).FirstOrDefaultAsync();
            if (data == null)
            {
                return ServiceResponse<FoundationPublicationDTO>.Return409("Bu ID'ye ait bir vakıf yayını bulunmamaktadır!");
            }
            else return ServiceResponse<FoundationPublicationDTO>.ReturnResultWith200(_mapper.Map<FoundationPublicationDTO>(data));
        }
    }
}
