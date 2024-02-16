using AutoMapper;
using ContentManagement.Data;
using ContentManagement.Data.Dto;
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
    public class GetAllChairmenQueryHandler : IRequestHandler<GetAllChairmenQuery, ServiceResponse<List<ChairmanDTO>>>
    {
        private readonly IChairmanRepository _repo;
        private readonly IMapper _mapper;

        public GetAllChairmenQueryHandler(IChairmanRepository chairmanRepository, IMapper mapper)
        {
            _repo = chairmanRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<ChairmanDTO>>> Handle(GetAllChairmenQuery request, CancellationToken cancellationToken)
        {
            var datas = await _repo.All.ToListAsync();
            return ServiceResponse<List<ChairmanDTO>>.ReturnResultWith200(_mapper.Map<List<ChairmanDTO>>(datas));
        }
    }
}
