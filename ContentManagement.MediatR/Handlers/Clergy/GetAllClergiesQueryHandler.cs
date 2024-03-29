﻿using AutoMapper;
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
    public class GetAllClergiesQueryHandler : IRequestHandler<GetAllClergiesQuery, ServiceResponse<List<ClergyDTO>>>
    {
        private readonly IClergyRepository _clergyRepository;
        private readonly IMapper _mapper;

        public GetAllClergiesQueryHandler(IClergyRepository clergyRepository, IMapper mapper)
        {
            _clergyRepository = clergyRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<ClergyDTO>>> Handle(GetAllClergiesQuery request, CancellationToken cancellationToken)
        {
            var datas = await _clergyRepository.All.ToListAsync();
            return ServiceResponse<List<ClergyDTO>>.ReturnResultWith200(_mapper.Map<List<ClergyDTO>>(datas));
        }
    }
}
