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
    public class GetHacPeriodListQueryHandler : IRequestHandler<GetHacPeriodListQuery, ServiceResponse<List<HacPeriodDTO>>>
    {
        private readonly IHacPeriodRepository _hacPeriodRepository;
        private readonly IMapper _mapper;

        public GetHacPeriodListQueryHandler(IHacPeriodRepository hacPeriodRepository, IMapper mapper)
        {
            _hacPeriodRepository = hacPeriodRepository;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<List<HacPeriodDTO>>> Handle(GetHacPeriodListQuery request, CancellationToken cancellationToken)
        {
            var list = await _hacPeriodRepository.All.ToListAsync();
            return ServiceResponse<List<HacPeriodDTO>>.ReturnResultWith200(_mapper.Map<List<HacPeriodDTO>>(list));
        }
    }
}
