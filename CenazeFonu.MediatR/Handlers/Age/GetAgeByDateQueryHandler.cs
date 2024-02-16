using AutoMapper;
using CenazeFonu.Data.Dto;
using CenazeFonu.Helper;
using CenazeFonu.MediatR.Queries;
using CenazeFonu.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CenazeFonu.MediatR.Handlers
{
    public class GetAgeByDateQueryHandler : IRequestHandler<GetAgeByDateQuery, ServiceResponse<AgeDTO>>
    {
        private readonly IAgeRepository _ageRepository;
        private readonly IMapper _mapper;

        public GetAgeByDateQueryHandler(IAgeRepository ageRepository, IMapper mapper)
        {
            _ageRepository = ageRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<AgeDTO>> Handle(GetAgeByDateQuery request, CancellationToken cancellationToken)
        {
            var age = DateTime.Now.Year - request.Date.Year;
            var db = _ageRepository.FindBy(x=>x.MinAge<= age && x.MaxAge>=age).FirstOrDefault();
            if (db == null)
            {
                return ServiceResponse<AgeDTO>.Return409("Yaş aralığına uygun fiyat bulunamadı!");
            }
            else return ServiceResponse<AgeDTO>.ReturnResultWith200(_mapper.Map<AgeDTO>(db));
        }
    }
}
