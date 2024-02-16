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
    public class GetServiceByIdQueryHandler : IRequestHandler<GetServiceByIdQuery, ServiceResponse<ServicesDTO>>
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IMapper _mapper;

        public GetServiceByIdQueryHandler(IServiceRepository serviceRepository, IMapper mapper)
        {
            _serviceRepository = serviceRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<ServicesDTO>> Handle(GetServiceByIdQuery request, CancellationToken cancellationToken)
        {
            var data = await _serviceRepository.All.Where(x => x.Id == request.Id && x.IsActive).FirstOrDefaultAsync();
            if (data == null)
            {
                return ServiceResponse<ServicesDTO>.Return409("Bu ID'ye ait aktif bir hizmet bulunmamaktadır!");
            }
            return ServiceResponse<ServicesDTO>.ReturnResultWith200(_mapper.Map<ServicesDTO>(data));
        }
    }
}
