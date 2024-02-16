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
    public class GetAllGendersQueryHandler : IRequestHandler<GetAllGendersQuery, ServiceResponse<List<GenderDTO>>>
    {
        private readonly IGenderRepository _genderRepository;
        private readonly IMapper _mapper;

        public GetAllGendersQueryHandler(IGenderRepository genderRepository, IMapper mapper)
        {
            _genderRepository = genderRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<GenderDTO>>> Handle(GetAllGendersQuery request, CancellationToken cancellationToken)
        {
            var genders = await _genderRepository.All.ToListAsync();
            return ServiceResponse<List<GenderDTO>>.ReturnResultWith200(_mapper.Map<List<GenderDTO>>(genders));
        }
    }
}
