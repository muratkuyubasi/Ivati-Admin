using AutoMapper;
using ContentManagement.Data.Dto;
using ContentManagement.DataDto;
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
    public class GetAllNewsQueryHandler : IRequestHandler<GetAllNewsQuery, ServiceResponse<List<NewsDTO>>>
    {
        private readonly INewsRepository repo;
        private readonly IMapper _mapper;

        public GetAllNewsQueryHandler(INewsRepository repository, IMapper mapper)
        {
            repo = repository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<NewsDTO>>> Handle(GetAllNewsQuery request, CancellationToken cancellationToken)
        {
            var datas = await repo.All.ToListAsync();
            return ServiceResponse<List<NewsDTO>>.ReturnResultWith200(_mapper.Map<List<NewsDTO>>(datas));
        }
    }
}
