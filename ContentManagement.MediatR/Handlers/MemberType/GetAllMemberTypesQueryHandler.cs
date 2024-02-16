using AutoMapper;
using ContentManagement.Data.Models;
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
    public class GetAllMemberTypesQueryHandler : IRequestHandler<GetAllMemberTypesQuery, ServiceResponse<List<MemberTypeDTO>>>
    {
        private readonly IMemberTypeRepository _memberTypeRepository;
        private readonly IMapper _mapper;

        public GetAllMemberTypesQueryHandler(IMemberTypeRepository memberTypeRepository, IMapper mapper)
        {
            _memberTypeRepository = memberTypeRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<MemberTypeDTO>>> Handle(GetAllMemberTypesQuery request, CancellationToken cancellationToken)
        {
            var datas = await _memberTypeRepository.All.ToListAsync();
            return ServiceResponse<List<MemberTypeDTO>>.ReturnResultWith200(_mapper.Map<List<MemberTypeDTO>>(datas));
        }
    }
}
