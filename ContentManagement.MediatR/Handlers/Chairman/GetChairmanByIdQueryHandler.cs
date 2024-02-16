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
    public class GetChairmanByIdQueryHandler : IRequestHandler<GetChairmanByIdQuery, ServiceResponse<ChairmanDTO>>
    {
        private readonly IChairmanRepository _repo;
        private readonly IMapper _mapper;

        public GetChairmanByIdQueryHandler(IChairmanRepository chairmanRepository, IMapper mapper)
        {
            _repo = chairmanRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<ChairmanDTO>> Handle(GetChairmanByIdQuery request, CancellationToken cancellationToken)
        {
            var data = await _repo.FindBy(x => x.Id == request.Id).FirstOrDefaultAsync();
            if (data == null)
            {
                return ServiceResponse<ChairmanDTO>.Return409("Bu ID'ye ait bir yönetim kurulu başkanı bulunmamaktadır!");
            }
            else return ServiceResponse<ChairmanDTO>.ReturnResultWith200(_mapper.Map<ChairmanDTO>(data));
        }
    }
}
