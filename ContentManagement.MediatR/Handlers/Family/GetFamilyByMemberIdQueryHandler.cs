using AutoMapper;
using AutoMapper.QueryableExtensions;
using ContentManagement.Common.UnitOfWork;
using ContentManagement.Data.Dto;
using ContentManagement.Data.Models;
using ContentManagement.Domain;
using ContentManagement.Helper;
using ContentManagement.MediatR.Queries;
using ContentManagement.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Polly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ContentManagement.MediatR.Handlers
{
    public class GetFamilyByMemberIdQueryHandler : IRequestHandler<GetFamilyByMemberIdQuery, ServiceResponse<FamilyDTO>>
    {
        private readonly IFamilyRepository _familyRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<PTContext> _uow;
        List<string> messages = new List<string>();

        public GetFamilyByMemberIdQueryHandler(IFamilyRepository familyRepository, IMapper mapper, IUnitOfWork<PTContext> uow)
        {
            _familyRepository = familyRepository;
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<ServiceResponse<FamilyDTO>> Handle(GetFamilyByMemberIdQuery request, CancellationToken cancellationToken)
        {
            var family = _familyRepository.All.Include(x => x.FamilyMembers).ThenInclude(x => x.MemberUser)
                .Include(x => x.Address).Include(x => x.Debtors).ThenInclude(x => x.DebtorType).Include(x => x.FamilyNotes).AsQueryable();

            if (!request.FamilyId.ToString().Equals("00000000-0000-0000-0000-000000000000"))
            {
                family = family.Where(
                x => x.Id == request.FamilyId);
            }
            if (request.ReferenceNumber > 0)
            {
                family = family.Where(x => x.ReferenceNumber == request.ReferenceNumber);
            }
            if (request.IsActive != null)
            {
                family = family.Where(x => x.IsActive == request.IsActive);
            }
            if (request.IsDeleted != null)
            {
                family = family.Where(x => x.IsDeleted == request.IsDeleted);
            }

            var f = family.FirstOrDefault();

            return ServiceResponse<FamilyDTO>.ReturnResultWith200(_mapper.Map<FamilyDTO>(f));
        }
    }
}
