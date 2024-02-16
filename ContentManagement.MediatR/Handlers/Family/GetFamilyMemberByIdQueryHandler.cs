using AutoMapper;
using ContentManagement.Data.Dto;
using ContentManagement.Helper;
using ContentManagement.MediatR.Queries;
using ContentManagement.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ContentManagement.MediatR.Handlers
{
    public class GetFamilyMemberByIdQueryHandler : IRequestHandler<GetFamilyMemberByIdQuery, ServiceResponse<UserInformationDTO>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetFamilyMemberByIdQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<UserInformationDTO>> Handle(GetFamilyMemberByIdQuery request, CancellationToken cancellationToken)
        {
            var user = _userRepository.All.Where(x=>x.Id == request.Id).FirstOrDefault();
            if (user == null)
            {
                return ServiceResponse<UserInformationDTO>.ReturnResultWith204();
            }
            else return ServiceResponse<UserInformationDTO>.ReturnResultWith200(_mapper.Map<UserInformationDTO>(user));
        }
    }
}
