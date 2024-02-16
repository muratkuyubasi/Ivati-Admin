using AutoMapper;
using CenazeFonu.Data.Dto;
using CenazeFonu.Helper;
using CenazeFonu.MediatR.Queries;
using CenazeFonu.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CenazeFonu.MediatR.Handlers
{
    public class GetAgeByIdQueryHandler : IRequestHandler<GetAgeByIdQuery, ServiceResponse<AgeDTO>>
    {
        private readonly IAgeRepository _ageRepository;
        private readonly IMapper _mapper;

        public GetAgeByIdQueryHandler(IAgeRepository ageRepository, IMapper mapper)
        {
            _ageRepository = ageRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<AgeDTO>> Handle(GetAgeByIdQuery request, CancellationToken cancellationToken)
        {
            var age = _ageRepository.All.Where(x => x.Id == request.Id).FirstOrDefault();
            if (age == null)
            {
                return ServiceResponse<AgeDTO>.Return409("Bu ID'ye ait bir yaş aralığı bulunamadı!");
            }
            else return ServiceResponse<AgeDTO>.ReturnResultWith200(_mapper.Map<AgeDTO>(age));
        }
    }
}
