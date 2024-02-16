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
    public class GetMosqueByIdQueryHandler : IRequestHandler<GetMosqueByIdQuery, ServiceResponse<MosqueDTO>>
    {
        private readonly IMosqueRepository repository;
        private readonly IMapper _mapper;

        public GetMosqueByIdQueryHandler(IMosqueRepository repo, IMapper mapper)
        {
            repository = repo;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<MosqueDTO>> Handle(GetMosqueByIdQuery request, CancellationToken cancellationToken)
        {
            var data = await repository.FindBy(x => x.Id == request.Id).FirstOrDefaultAsync();
            if (data == null)
            {
                return ServiceResponse<MosqueDTO>.Return409("Bu ID'ye ait bir cami bulunmamaktadır!");
            }
            else return ServiceResponse<MosqueDTO>.ReturnResultWith200(_mapper.Map<MosqueDTO>(data));
        }
    }
}
