using AutoMapper;
using ContentManagement.Data.Dto;
using ContentManagement.MediatR.Queries;
using ContentManagement.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ContentManagement.Helper;

namespace ContentManagement.MediatR.Handlers
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
