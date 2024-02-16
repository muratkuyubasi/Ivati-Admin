using AutoMapper;
using CenazeFonu.Data.Dto;
using CenazeFonu.MediatR.Queries;
using CenazeFonu.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CenazeFonu.Helper;
using System.Linq.Dynamic.Core;
using System.Linq;

namespace CenazeFonu.MediatR.Handlers
{
    public class GetUserDetailByIdQueryHandler : IRequestHandler<GetUserDetailByIdQuery, ServiceResponse<UserDetailDTO>>
    {
        private readonly IUserDetailRepository _userDetailRepository;
        private readonly IMapper _mapper;

        public GetUserDetailByIdQueryHandler(
            IUserDetailRepository userDetailRepository,
            IMapper mapper)
        {
            _userDetailRepository = userDetailRepository;
            _mapper = mapper;

        }
        public async Task<ServiceResponse<UserDetailDTO>> Handle(GetUserDetailByIdQuery request, CancellationToken cancellationToken)
        {
            var data = await _userDetailRepository.FindBy(x => x.UserId == request.Id).FirstOrDefaultAsync();
            return ServiceResponse<UserDetailDTO>.ReturnResultWith200(_mapper.Map<UserDetailDTO>(data));
        }
    }
}
