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
    public class GetFamilyByUserIdQueryHandler : IRequestHandler<GetFamilyByUserIdQuery, ServiceResponse<FamilyDTO>>
    {
        private readonly IFamilyRepository _familyRepository;
        private readonly IMapper _mapper;

        public GetFamilyByUserIdQueryHandler(IFamilyRepository familyRepository, IMapper mapper)
        {
            _familyRepository = familyRepository;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<FamilyDTO>> Handle(GetFamilyByUserIdQuery request, CancellationToken cancellationToken)
        {
            var family = await _familyRepository.All
                .Include(x=>x.FamilyMembers).ThenInclude(x=>x.MemberUser)
                .Include(x=>x.Address).Include(x=>x.Address)
                .Include(x=>x.FamilyNotes)
                .Include(x=>x.Debtors).ThenInclude(x=>x.DebtorType)
                .Where(x => x.UserId == request.Id).FirstOrDefaultAsync();
            return ServiceResponse<FamilyDTO>.ReturnResultWith200(_mapper.Map<FamilyDTO>(family));
        }
    }
}
