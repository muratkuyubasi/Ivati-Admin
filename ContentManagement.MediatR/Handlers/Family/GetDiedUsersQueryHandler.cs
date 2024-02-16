using AutoMapper;
using ContentManagement.Data.Dto;
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
using System.Linq.Dynamic.Core;

namespace ContentManagement.MediatR.Handlers
{
    public class GetDiedUsersQueryHandler : IRequestHandler<GetDiedUsersQuery, ServiceResponse<DiedFamilyMemberPaginationDTO>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IFamilyMemberRepository _familyMemberRepository;
        private readonly IMapper _mapper;

        public GetDiedUsersQueryHandler(IUserRepository userRepository, IMapper mapper, IFamilyMemberRepository familyMemberRepository)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _familyMemberRepository = familyMemberRepository;
        }

        public async Task<ServiceResponse<DiedFamilyMemberPaginationDTO>> Handle(GetDiedUsersQuery request, CancellationToken cancellationToken)
        {
            var data = _familyMemberRepository.All.Include(x => x.MemberUser).Include(x => x.Family)
                .Select(X => new DiedFamilyMemberDTO
                {
                    Id = X.Family.Id,
                    MemberUserId = X.MemberUserId,
                    FullName = X.MemberUser.FirstName + " " + X.MemberUser.LastName,
                    BurialPlace = X.MemberUser.BurialPlace,
                    PersonNo = X.MemberUser.IdentificationNumber,
                    PlaceOfDeath = X.MemberUser.PlaceOfDeath,
                    DateOfDeath = X.MemberUser.DateOfDeath.Value.Day.ToString() + "." + X.MemberUser.DateOfDeath.Value.Month.ToString() + "." + X.MemberUser.DateOfDeath.Value.Year.ToString(),
                    FamilyId = X.Family.MemberId,
                    IsDead = X.MemberUser.IsDead
                }).Where(X => X.IsDead == true).AsQueryable();
            if (request.Search != null)
            {
                data = data.Where(x => x.FullName.ToUpper().StartsWith(request.Search.ToUpper())
                || x.PersonNo.StartsWith(request.Search)
                || x.PlaceOfDeath.ToUpper().StartsWith(request.Search.ToUpper())
                || x.DateOfDeath.StartsWith(request.Search)
                || x.BurialPlace.ToUpper().StartsWith(request.Search.ToUpper())
                || x.MemberUserId.ToString().StartsWith(request.Search)
                || x.FamilyId.ToString().StartsWith(request.Search));
            }
            if (request.OrderBy != null)
            {
                data = data.OrderBy(request.OrderBy);
            }
            else { data = data.OrderBy(x=>x.FamilyId); }
            var paginationData = await data.Skip(request.Skip * request.PageSize).Take(request.PageSize)
                .OrderBy(x => x.FamilyId).ToListAsync();

            var list = new DiedFamilyMemberPaginationDTO
            {
                Data = _mapper.Map<List<DiedFamilyMemberDTO>>(paginationData),
                Skip = request.Skip,
                TotalCount = data.Count(),
                PageSize = request.PageSize,
            };
            return ServiceResponse<DiedFamilyMemberPaginationDTO>.ReturnResultWith200(list);
        }
    }
}
