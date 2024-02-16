using AutoMapper;
using CenazeFonu.Common.UnitOfWork;
using CenazeFonu.Data.Dto;
using CenazeFonu.Domain;
using CenazeFonu.Helper;
using CenazeFonu.MediatR.Queries;
using CenazeFonu.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CenazeFonu.MediatR.Handlers
{
    public class GetFamilyMembersQueryHandler : IRequestHandler<GetAllFamiliesQuery, ServiceResponse<List<FamilyDTO>>>
    {
        private readonly IFamilyMemberRepository _familyMemberRepository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork<PTContext> _uow;
        private readonly IFamilyRepository _familyRepository;

        public GetFamilyMembersQueryHandler(IFamilyMemberRepository familyMemberRepository, IMapper mapper, IUserRepository userRepository, IUnitOfWork<PTContext> uow, IFamilyRepository familyRepository)
        {
            _familyMemberRepository = familyMemberRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _uow = uow;
            _familyRepository = familyRepository;
        }

        public async Task<ServiceResponse<List<FamilyDTO>>> Handle(GetAllFamiliesQuery request, CancellationToken cancellationToken)
        {
            var members = _familyRepository.All
                .Include(a => a.Address)
                                .Include(x => x.Debtors)
                                .Include(x => x.FamilyMembers)
                .ThenInclude(x=>x.MemberUser)
                .ToListAsync();
            return ServiceResponse<List<FamilyDTO>>.ReturnResultWith200(_mapper.Map<List<FamilyDTO>>(members));

        }
    }
}
