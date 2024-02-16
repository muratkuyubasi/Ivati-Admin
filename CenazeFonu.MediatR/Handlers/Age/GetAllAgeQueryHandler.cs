using AutoMapper;
using CenazeFonu.Common.UnitOfWork;
using CenazeFonu.Data.Dto;
using CenazeFonu.Domain;
using CenazeFonu.Helper;
using CenazeFonu.MediatR.Queries;
using CenazeFonu.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CenazeFonu.MediatR.Handlers
{
    public class GetAllAgeQueryHandler : IRequestHandler<GetAllAgeQuery, ServiceResponse<List<AgeDTO>>>
    {
        private readonly IAgeRepository _ageRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<PTContext> _uow;

        public GetAllAgeQueryHandler(IAgeRepository ageRepository, IMapper mapper, IUnitOfWork<PTContext> uow)
        {
            _ageRepository = ageRepository;
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<ServiceResponse<List<AgeDTO>>> Handle(GetAllAgeQuery request, CancellationToken cancellationToken)
        {
            var age = await _ageRepository.All.ToListAsync();
            return ServiceResponse<List<AgeDTO>>.ReturnResultWith200(_mapper.Map<List<AgeDTO>>(age));
        }
    }
}
