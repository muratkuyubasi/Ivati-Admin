using AutoMapper;
using CenazeFonu.Data.Dto;
using CenazeFonu.Helper;
using CenazeFonu.MediatR.Queries;
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
    public class GetAllFoundationPublicationQueryHandler : IRequestHandler<GetAllFoundationPublicationQuery, ServiceResponse<List<FoundationPublicationDTO>>>
    {
        private readonly IFoundationPublicationRepository repo;
        private readonly IMapper _mapper;

        public GetAllFoundationPublicationQueryHandler(IFoundationPublicationRepository repository, IMapper mapper)
        {
            repo = repository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<FoundationPublicationDTO>>> Handle(GetAllFoundationPublicationQuery request, CancellationToken cancellationToken)
        {
            var datas = await repo.All.ToListAsync();
            return ServiceResponse<List<FoundationPublicationDTO>>.ReturnResultWith200(_mapper.Map<List<FoundationPublicationDTO>>(datas));
        }
    }
}
