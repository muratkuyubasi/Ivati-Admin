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
    public class GetUmrePeriodListQueryHandler : IRequestHandler<GetUmrePeriodListQuery, ServiceResponse<List<UmrePeriodDTO>>>
    {
        private readonly IUmrePeriodRepository _umrePeriodRepository;
        private readonly IMapper _mapper;

        public GetUmrePeriodListQueryHandler(IUmrePeriodRepository umrePeriodRepository, IMapper mapper)
        {
            _umrePeriodRepository = umrePeriodRepository;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<List<UmrePeriodDTO>>> Handle(GetUmrePeriodListQuery request, CancellationToken cancellationToken)
        {
            var list = await _umrePeriodRepository.All.ToListAsync();
            return ServiceResponse<List<UmrePeriodDTO>>.ReturnResultWith200(_mapper.Map<List<UmrePeriodDTO>>(list));
        }
    }
}
