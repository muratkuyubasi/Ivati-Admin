using AutoMapper;
using ContentManagement.Data.Dto;
using ContentManagement.MediatR.Queries;
using ContentManagement.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ContentManagement.Helper;
using System.Linq.Dynamic.Core;
using System.Linq;

namespace ContentManagement.MediatR.Handlers
{
    public class GetAllUserDetailQueryHandler : IRequestHandler<GetAllUserDetailQuery, ServiceResponse<List<UserDetailDTO>>>
    {
        private readonly IUserDetailRepository _userDetailRepository;
        private readonly IMapper _mapper;

        public GetAllUserDetailQueryHandler(
            IUserDetailRepository userDetailRepository,
            IMapper mapper)
        {
            _userDetailRepository = userDetailRepository;
            _mapper = mapper;

        }
        public async Task<ServiceResponse<List<UserDetailDTO>>> Handle(GetAllUserDetailQuery request, CancellationToken cancellationToken)
        {
            var entities = await _userDetailRepository.All.ToListAsync();
            return ServiceResponse<List<UserDetailDTO>>.ReturnResultWith200(_mapper.Map<List<UserDetailDTO>>(entities));
        }
    }
}
