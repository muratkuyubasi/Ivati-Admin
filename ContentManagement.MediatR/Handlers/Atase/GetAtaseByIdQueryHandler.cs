using AutoMapper;
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
    public class GetAtaseByIdQueryHandler : IRequestHandler<GetAtaseByIdQuery, ServiceResponse<AtaseModelDTO>>
    {
        private readonly IAtaseRepository _repo;
        private readonly IMapper _mapper;

        public GetAtaseByIdQueryHandler(IAtaseRepository ataseRepository, IMapper mapper)
        {
            _repo = ataseRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<AtaseModelDTO>> Handle(GetAtaseByIdQuery request, CancellationToken cancellationToken)
        {
            var data = await _repo.FindBy(x => x.Id == request.Id).FirstOrDefaultAsync();
            if (data == null)
            {
                return ServiceResponse<AtaseModelDTO>.Return409("Bu ID'ye ait bir ataşe görevlisi bulunmamaktadır!");
            }
            else return ServiceResponse<AtaseModelDTO>.ReturnResultWith200(_mapper.Map<AtaseModelDTO>(data));
        }
    }
}
