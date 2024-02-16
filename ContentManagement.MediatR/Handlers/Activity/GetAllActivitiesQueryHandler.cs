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
    public class GetAllActivitiesQueryHandler : IRequestHandler<GetAllActivitiesQuery, ServiceResponse<List<ActivityDTO>>>
    {
        private readonly IActivityRepository _activityRepository;
        private readonly IMapper _mapper;

        public GetAllActivitiesQueryHandler(IActivityRepository activityRepository, IMapper mapper)
        {
            _activityRepository = activityRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<ActivityDTO>>> Handle(GetAllActivitiesQuery request, CancellationToken cancellationToken)
        {
            var activities = await _activityRepository.All.Where(x=>x.IsActive).ToListAsync();
            return ServiceResponse<List<ActivityDTO>>.ReturnResultWith200(_mapper.Map<List<ActivityDTO>>(activities));
        }
    }
}
