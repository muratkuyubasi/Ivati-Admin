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
    public class GetAllServicesQueryHandler : IRequestHandler<GetAllServicesQuery, ServiceResponse<List<ServicesDTO>>>
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IMapper _mapper;

        public GetAllServicesQueryHandler(IServiceRepository serviceRepository, IMapper mapper)
        {
            _serviceRepository = serviceRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<ServicesDTO>>> Handle(GetAllServicesQuery request, CancellationToken cancellationToken)
        {
            var datas = await _serviceRepository.All.Where(x=>x.IsActive).ToListAsync();
            return ServiceResponse<List<ServicesDTO>>.ReturnResultWith200(_mapper.Map<List<ServicesDTO>>(datas));
        }
    }
}
