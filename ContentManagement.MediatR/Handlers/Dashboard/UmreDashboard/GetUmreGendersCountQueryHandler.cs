using AutoMapper;
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

namespace ContentManagement.MediatR.Handlers.Dashboard.HacDashboard
{
    public class GetUmreGendersCountQueryHandler : IRequestHandler<GetUmreGendersCountQuery, Object>
    {
        private readonly IUmreFormRepository _repo;
        private readonly IMapper _mapper;

        public GetUmreGendersCountQueryHandler(IUmreFormRepository hacRepository, IMapper mapper)
        {
            _repo = hacRepository;
            _mapper = mapper;
        }
        public async Task<object> Handle(GetUmreGendersCountQuery request, CancellationToken cancellationToken)
        {
            var mencount = _repo.All.Where(x=>x.GenderId == 2).Count();
            var womencount = _repo.All.Where(x=>x.GenderId == 1).Count();
            return new { mencount, womencount };         
        }
    }
}
