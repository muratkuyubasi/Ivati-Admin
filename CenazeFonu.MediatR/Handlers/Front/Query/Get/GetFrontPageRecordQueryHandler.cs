﻿using AutoMapper;
using CenazeFonu.Data.Dto;
using CenazeFonu.MediatR.Queries;
using CenazeFonu.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using CenazeFonu.Helper;
using Microsoft.EntityFrameworkCore;

namespace CenazeFonu.Mediatr.Handlers
{
    public class GetFrontPageRecordQueryHandler : IRequestHandler<GetFrontPageRecordQuery, ServiceResponse<FrontPageRecordDto>>
    {
        private readonly IFrontPageRecordRepository _FrontPageRecordRepository;
        private readonly IMapper _mapper;

        public GetFrontPageRecordQueryHandler(
           IFrontPageRecordRepository FrontPageRecordRepository,
           IMapper mapper)
        {
            _FrontPageRecordRepository = FrontPageRecordRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<FrontPageRecordDto>> Handle(GetFrontPageRecordQuery request, CancellationToken cancellationToken)
        {
            var entity = await _FrontPageRecordRepository.FindBy(x => x.Code == request.Code).FirstOrDefaultAsync();
            if (entity != null)
                return ServiceResponse<FrontPageRecordDto>.ReturnResultWith200(_mapper.Map<FrontPageRecordDto>(entity));
            else
                return ServiceResponse<FrontPageRecordDto>.Return404();
        }
    }
}