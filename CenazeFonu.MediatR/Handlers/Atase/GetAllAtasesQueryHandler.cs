﻿using AutoMapper;
using CenazeFonu.Data.Dto;
using CenazeFonu.Data.Dto;
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
    public class GetAllAtasesQueryHandler : IRequestHandler<GetAllAtasesQuery, ServiceResponse<List<AtaseModelDTO>>>
    {
        private readonly IAtaseRepository _repo;
        private readonly IMapper _mapper;

        public GetAllAtasesQueryHandler(IAtaseRepository ataseRepository, IMapper mapper)
        {
            _repo = ataseRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<AtaseModelDTO>>> Handle(GetAllAtasesQuery request, CancellationToken cancellationToken)
        {
            var datas = await _repo.All.ToListAsync();
            return ServiceResponse<List<AtaseModelDTO>>.ReturnResultWith200(_mapper.Map<List<AtaseModelDTO>>(datas));
        }
    }
}