﻿using AutoMapper;
using CenazeFonu.Data.Dto;
using CenazeFonu.MediatR.Queries;
using CenazeFonu.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CenazeFonu.MediatR.Handlers
{
    public class GetAllPageActionQueryHandler : IRequestHandler<GetAllPageActionQuery, List<PageActionDto>>
    {
        private readonly IPageActionRepository _pageActionRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllPageActionQueryHandler> _logger;

        public GetAllPageActionQueryHandler(
          IPageActionRepository pageActionRepository,
            IMapper mapper,
            ILogger<GetAllPageActionQueryHandler> logger)
        {
            _pageActionRepository = pageActionRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<PageActionDto>> Handle(GetAllPageActionQuery request, CancellationToken cancellationToken)
        {
            var entities = await _pageActionRepository.All.ToListAsync();
            return _mapper.Map<List<PageActionDto>>(entities);
        }
    }
}
