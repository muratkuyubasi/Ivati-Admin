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
    public class GetProjectByIdQueryHandler : IRequestHandler<GetProjectByIdQuery, ServiceResponse<ProjectDTO>>
    {
        private readonly IProjectRepository repo;
        private readonly IMapper _map;

        public GetProjectByIdQueryHandler(IProjectRepository translationRepository, IMapper mapper)
        {
            repo = translationRepository;
            _map = mapper;
        }

        public async Task<ServiceResponse<ProjectDTO>> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
        {
            var data = await repo.FindBy(x => x.Id == request.Id).FirstOrDefaultAsync();
            if (data == null)
            {
                return ServiceResponse<ProjectDTO>.Return409("Bu ID'ye ait bir proje bulunmamaktadır!");
            }
            else return ServiceResponse<ProjectDTO>.ReturnResultWith200(_map.Map<ProjectDTO>(data));
        }
    }
}
