using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ContentManagement.Data.Dto;
using ContentManagement.Helper;
using ContentManagement.MediatR.Queries;
using ContentManagement.Repository;

namespace ContentManagement.MediatR.Handlers
{
    public class GetAllAppSettingQueryHandler : IRequestHandler<GetAllAppSettingQuery, ServiceResponse<List<AppSettingDto>>>
    {
        private readonly IAppSettingRepository _appSettingRepository;
        private readonly IMapper _mapper;
        public GetAllAppSettingQueryHandler(
           IAppSettingRepository appSettingRepository,
            IMapper mapper
            )
        {
            _appSettingRepository = appSettingRepository;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<List<AppSettingDto>>> Handle(GetAllAppSettingQuery request, CancellationToken cancellationToken)
        {
            
            var entities = await _appSettingRepository.All.ToListAsync();
            return ServiceResponse<List<AppSettingDto>>.ReturnResultWith200(_mapper.Map<List<AppSettingDto>>(entities));
        }
    }
}
