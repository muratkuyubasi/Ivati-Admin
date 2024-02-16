using AutoMapper;
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
    public class GetAllMosquesQueryHandler : IRequestHandler<GetAllMosquesQuery, ServiceResponse<List<MosqueDTO>>>
    {
        private readonly IMosqueRepository repo;
        private readonly IMapper _mapper;

        public GetAllMosquesQueryHandler(IMosqueRepository repository, IMapper mapper)
        {
            repo = repository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<MosqueDTO>>> Handle(GetAllMosquesQuery request, CancellationToken cancellationToken)
        {
            var datas = await repo.All.ToListAsync();
            return ServiceResponse<List<MosqueDTO>>.ReturnResultWith200(_mapper.Map<List<MosqueDTO>>(datas));
        }
    }
}
