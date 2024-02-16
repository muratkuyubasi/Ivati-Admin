using AutoMapper;
using CenazeFonu.Data.Dto;
using CenazeFonu.Helper;
using CenazeFonu.MediatR.Queries;
using CenazeFonu.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CenazeFonu.MediatR.Handlers
{
    public class GetAllUserCountQueryHandler : IRequestHandler<GetAllUserCountQuery, ServiceResponse<int>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetAllUserCountQueryHandler(
           IUserRepository userRepository,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<int>> Handle(GetAllUserCountQuery request, CancellationToken cancellationToken)
        {
            var entities = await _userRepository.All.CountAsync();
            return ServiceResponse<int>.ReturnResultWith200(entities);

        }
    }
}
