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
    public class GetAgeByMaxAndMinValueQueryHandler : IRequestHandler<GetAgeByMaxAndMinValueQuery, ServiceResponse<AgeDTO>>
    {
        private readonly IAgeRepository _ageRepository;
        private readonly IMapper _mapper;

        public GetAgeByMaxAndMinValueQueryHandler(IAgeRepository ageRepository, IMapper mapper)
        {
            _ageRepository = ageRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<AgeDTO>> Handle(GetAgeByMaxAndMinValueQuery request, CancellationToken cancellationToken)
        {
            var age = await _ageRepository.All.Where(x => x.MinAge == request.MinAge && x.MaxAge == request.MaxAge).FirstOrDefaultAsync();
            if (age == null)
            {
                return ServiceResponse<AgeDTO>.Return409("Bu yaş aralıklarına göre bir miktar bulunamamaktadır!");
            }
            else return ServiceResponse<AgeDTO>.ReturnResultWith200(_mapper.Map<AgeDTO>(age));
        }
    }
}
