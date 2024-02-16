using AutoMapper;
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
    public class GetAllProjectsQueryHandler : IRequestHandler<GetProjectsQuery, ServiceResponse<List<ProjectDTO>>>
    {
        private readonly IProjectRepository repo;
        private readonly IMapper _map;

        public GetAllProjectsQueryHandler(IProjectRepository cityRepository, IMapper mapper)
        {
            repo = cityRepository;
            _map = mapper;
        }

        public async Task<ServiceResponse<List<ProjectDTO>>> Handle(GetProjectsQuery request, CancellationToken cancellationToken)
        {
            var cities = await repo.All.Include(x => x.Language).ToListAsync();
            return ServiceResponse<List<ProjectDTO>>.ReturnResultWith200(_map.Map<List<ProjectDTO>>(cities));
        }
    }
}
