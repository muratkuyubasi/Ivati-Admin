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
