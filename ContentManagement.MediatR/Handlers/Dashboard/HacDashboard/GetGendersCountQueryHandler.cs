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
    public class GetGendersCountQueryHandler : IRequestHandler<GetGendersCountQuery, Object>
    {
        private readonly IHacRepository _hacRepository;
        private readonly IMapper _mapper;

        public GetGendersCountQueryHandler(IHacRepository hacRepository, IMapper mapper)
        {
            _hacRepository = hacRepository;
            _mapper = mapper;
        }
        public async Task<object> Handle(GetGendersCountQuery request, CancellationToken cancellationToken)
        {
            var mencount = _hacRepository.All.Where(x=>x.GenderId == 2).Count();
            var womencount = _hacRepository.All.Where(x=>x.GenderId == 1).Count();
            return new { mencount, womencount };         
        }
    }
}
