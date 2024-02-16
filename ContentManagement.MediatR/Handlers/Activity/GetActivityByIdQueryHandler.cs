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
    public class GetActivityByIdQueryHandler : IRequestHandler<GetActivityByIdQuery, ServiceResponse<ActivityDTO>>
    {
        private readonly IActivityRepository _repo;
        private readonly IMapper _mapper;

        public GetActivityByIdQueryHandler(IActivityRepository activityRepository, IMapper mapper)
        {
            _repo = activityRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<ActivityDTO>> Handle(GetActivityByIdQuery request, CancellationToken cancellationToken)
        {
            var data = await _repo.FindBy(x => x.Id == request.Id && x.IsActive).FirstOrDefaultAsync();
            if (data == null)
            {
                return ServiceResponse<ActivityDTO>.Return409("Bu ID'ye ait bir faaliyet bulunmamaktadır!");
            }
            else return ServiceResponse<ActivityDTO>.ReturnResultWith200(_mapper.Map<ActivityDTO>(data));
        }
    }
}
