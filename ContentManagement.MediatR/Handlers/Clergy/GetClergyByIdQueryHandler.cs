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
    public class GetClergyByIdQueryHandler : IRequestHandler<GetClergyByIdQuery, ServiceResponse<ClergyDTO>>
    {
        private readonly IClergyRepository _clergyRepository;
        private readonly IMapper _mapper;

        public GetClergyByIdQueryHandler(IClergyRepository clergyRepository, IMapper mapper)
        {
            _clergyRepository = clergyRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<ClergyDTO>> Handle(GetClergyByIdQuery request, CancellationToken cancellationToken)
        {
            var data = await _clergyRepository.FindBy(x => x.Id == request.Id).FirstOrDefaultAsync();
            if (data == null)
            {
                return ServiceResponse<ClergyDTO>.Return409("Bu ID'ye ait bir din görevlisi bulunmamaktadır!");
            }
            else return ServiceResponse<ClergyDTO>.ReturnResultWith200(_mapper.Map<ClergyDTO>(data));
        }
    }
}
