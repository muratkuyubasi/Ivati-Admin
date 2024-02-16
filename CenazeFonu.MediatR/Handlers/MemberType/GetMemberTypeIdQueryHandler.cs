using AutoMapper;
using CenazeFonu.Data.Models;
using CenazeFonu.Helper;
using CenazeFonu.MediatR.Queries;
using CenazeFonu.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CenazeFonu.MediatR.Handlers
{
    public class GetMemberTypeIdQueryHandler : IRequestHandler<GetMemberTypeByIdQuery, ServiceResponse<MemberTypeDTO>>
    {
        private readonly IMemberTypeRepository  _memberTypeRepository;
        private readonly IMapper _mapper;

        public GetMemberTypeIdQueryHandler(IMemberTypeRepository memberTypeRepository, IMapper mapper)
        {
            _memberTypeRepository = memberTypeRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<MemberTypeDTO>> Handle(GetMemberTypeByIdQuery request, CancellationToken cancellationToken)
        {
            var data = _memberTypeRepository.FindBy(x=>x.Id == request.Id).FirstOrDefault();
            if (data == null)
            {
                return ServiceResponse<MemberTypeDTO>.Return409("Bu ID'ye ait bir kullanıcı tipi bulunamadı!");
            }
            else return ServiceResponse<MemberTypeDTO>.ReturnResultWith200(_mapper.Map<MemberTypeDTO>(data));
        }
    }
}
