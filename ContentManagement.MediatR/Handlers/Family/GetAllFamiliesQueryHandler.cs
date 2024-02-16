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
    public class GetAllFamiliesQueryHandler : IRequestHandler<GetAllFamiliesQuery, ServiceResponse<List<FamilyDTO>>>
    {
        private readonly IFamilyRepository _familyRepository;
        private readonly IMapper _mapper;

        public GetAllFamiliesQueryHandler(IFamilyRepository familyRepository, IMapper mapper)
        {
            _familyRepository = familyRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<FamilyDTO>>> Handle(GetAllFamiliesQuery request, CancellationToken cancellationToken)
        {
            var families = await _familyRepository.All.Take(request.Distance).ToListAsync();
            return ServiceResponse<List<FamilyDTO>>.ReturnResultWith200(_mapper.Map<List<FamilyDTO>>(families));
        }
    }
}
