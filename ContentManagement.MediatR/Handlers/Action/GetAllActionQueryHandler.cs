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
    public class GetAllActionQueryHandler : IRequestHandler<GetAllActionQuery, ServiceResponse<List<ActionDto>>>
    {
        private readonly IActionRepository _actionRepository;
        private readonly IMapper _mapper;

        public GetAllActionQueryHandler(
            IActionRepository actionRepository,
            IMapper mapper)
        {
            _actionRepository = actionRepository;
            _mapper = mapper;

        }
        public async Task<ServiceResponse<List<ActionDto>>> Handle(GetAllActionQuery request, CancellationToken cancellationToken)
        {
            var entities = await _actionRepository.All.ToListAsync();
            return ServiceResponse<List<ActionDto>>.ReturnResultWith200(_mapper.Map<List<ActionDto>>(entities));
        }
    }
}
