using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ContentManagement.MediatR.Queries;
using ContentManagement.Helper;
using ContentManagement.Repository;
using ContentManagement.Data.Dto;

namespace IsvecDiyanet.MediatR.Handlers
{
    public class GetAllFamilyMemberPaginationQueryHandler : IRequestHandler<GetFamilyMembersPaginationQuery, ServiceResponse<FamilyMemberPaginationDto>>
    {
        private readonly IFamilyMemberRepository _repo;

        private readonly IMapper _mapper;

        public GetAllFamilyMemberPaginationQueryHandler(IFamilyMemberRepository familyRepository, IMapper mapper)
        {
            _repo = familyRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<FamilyMemberPaginationDto>> Handle(GetFamilyMembersPaginationQuery request, CancellationToken cancellationToken)
        {
            //var d = "";

            //var family = _familyRepository.AllIncluding(x => x.FamilyMembers, a => a.Address, d => d.Debtors).Select(s => new FamilyDTO
            //{
            //    Address = _mapper.Map<AddressDTO>(s.Address),
            //    Debtors = _mapper.Map<List<DebtorDTO>>(s.Debtors),
            //    FamilyMembers = s.FamilyMembers.Select(fm => new FamilyMemberDTO
            //    {
            //        Id = fm.Id,
            //        MemberUser = _mapper.Map<UserInformationDTO>(fm.MemberUser),
            //    }).ToList()
            //});

            var family = _repo.All
                .Include(x => x.Family).ThenInclude(x=>x.User)
                .Include(x => x.MemberType)
                .Include(X => X.MemberUser).ThenInclude(x=>x.Gender).Where(x => x.MemberTypeId == 2 || x.MemberTypeId == 3)
                .Select(x => new FamilyMemberSimpleDTO
                {
                    Id = x.MemberUser.Id,
                    FamilyId = x.Family.MemberId,
                    IsActive = x.MemberUser.IsActive,
                    IsDeleted = x.MemberUser.IsDeleted,
                    MemberAge = DateTime.Now.Year - x.MemberUser.BirthDay.Value.Year,
                    MemberTypeId = x.MemberType.Id,
                    FirstName = x.MemberUser.FirstName.ToUpper(),
                    LastName = x.MemberUser.LastName.ToUpper(),
                    BirthDate = x.MemberUser.BirthDay.Value.Day.ToString() + "/" + x.MemberUser.BirthDay.Value.Month.ToString() + "/" + x.MemberUser.BirthDay.Value.Year.ToString() ?? null,
                    HouseHolderName = x.Family.User.FirstName.ToUpper() + " " + x.Family.User.LastName.ToUpper(),
                    BirthPlace = x.MemberUser.BirthPlace.ToUpper(),
                    GenderId = x.MemberUser.Gender.Id,
                    CityId = (int)x.Family.CityId
                }).AsNoTracking();

            if (request.Search != null)
            {
                family = family.Where(x => x.FirstName.ToUpper().StartsWith(request.Search.ToUpper()) 
                || x.LastName.ToUpper().StartsWith(request.Search.ToUpper()) 
                || x.BirthPlace.ToUpper().StartsWith(request.Search.ToString().ToUpper()) 
                || x.GenderId.Equals(request.Search)
                || x.MemberTypeId.Equals(request.Search)
                || x.HouseHolderName.ToUpper().StartsWith(request.Search.ToString().ToUpper()) 
                || x.BirthDate.StartsWith(request.Search));
            }
            if (request.AileFertlerineGoreSirala != null)
            {
                //https://stackoverflow.com/questions/2779375/order-a-list-c-by-many-fields
                family = family.OrderBy(x => x.FamilyId).ThenBy(X => X.HouseHolderName);
            }
            if (!string.IsNullOrEmpty(request.OrderBy))
            {
                family = family.OrderBy(request.OrderBy);
            }
            if (request.CityId != 0)
            {
                family = family.Where(x => x.CityId == request.CityId);
            }

            if (request.Erkek21Yas != null)
            {
                family = family.Where(x => x.GenderId == 2 && x.MemberAge >= 21 && x.MemberTypeId == 3);
            }
            if (request.Kadin23Yas != null)
            {
                family = family.Where(X => X.GenderId == 2 && X.MemberAge >= 23 && X.MemberTypeId == 3);
            }

            var paginationData = family.AsEnumerable().Skip(request.Skip * request.PageSize).Take(request.PageSize).ToList();

            var list = new FamilyMemberPaginationDto
            {
                Data = _mapper.Map<List<FamilyMemberSimpleDTO>>(paginationData),
                Skip = request.Skip,
                TotalCount = family.Count(),
                PageSize = request.PageSize,
            };
            return ServiceResponse<FamilyMemberPaginationDto>.ReturnResultWith200(list);
        }
        //public bool checkNull(bool? a, bool? b)
        //{
        //    if (a == null && b == null)
        //    {
        //        return true;
        //    }
        //    else return false;
        //}
    }

}
