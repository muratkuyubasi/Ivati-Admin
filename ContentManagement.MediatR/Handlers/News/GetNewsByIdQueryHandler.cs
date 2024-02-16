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
    public class GetNewsByIdQueryHandler : IRequestHandler<GetNewsByIdQuery, ServiceResponse<NewsDTO>>
    {
        private readonly INewsRepository repository;
        private readonly IMapper _mapper;

        public GetNewsByIdQueryHandler(INewsRepository repo, IMapper mapper)
        {
            repository = repo;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<NewsDTO>> Handle(GetNewsByIdQuery request, CancellationToken cancellationToken)
        {
            var data = await repository.FindBy(x => x.Id == request.Id).FirstOrDefaultAsync();
            if (data == null)
            {
                return ServiceResponse<NewsDTO>.Return409("Bu ID'ye ait bir haber bulunmamaktadır!");
            }
            else return ServiceResponse<NewsDTO>.ReturnResultWith200(_mapper.Map<NewsDTO>(data));
        }
    }
}
