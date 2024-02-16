using AutoMapper;
using CenazeFonu.Data.Dto;
using CenazeFonu.MediatR.Queries;
using CenazeFonu.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CenazeFonu.Helper;

namespace CenazeFonu.MediatR.Handlers
{
    public class GetAllAssociationQueryHandler : IRequestHandler<GetAllAssociationQuery, ServiceResponse<List<AssociationDTO>>>
    {
        private readonly IAssociationRepository _associationRepository;
        private readonly IMapper _mapper;

        public GetAllAssociationQueryHandler(
            IAssociationRepository associationRepository,
            IMapper mapper)
        {
            _associationRepository = associationRepository;
            _mapper = mapper;

        }
        public async Task<ServiceResponse<List<AssociationDTO>>> Handle(GetAllAssociationQuery request, CancellationToken cancellationToken)
        {
            var entities = await _associationRepository.All.ToListAsync();
            return ServiceResponse<List<AssociationDTO>>.ReturnResultWith200(_mapper.Map<List<AssociationDTO>>(entities));
        }
    }
}
