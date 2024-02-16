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
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;

namespace ContentManagement.MediatR.Handlers
{
    public class GetAllFamiliesPaginationQueryHandler : IRequestHandler<GetAllFamiliesPaginationQuery, ServiceResponse<FamilyPaginationDto>>
    {
        private readonly IFamilyRepository _familyRepository;

        private readonly IMapper _mapper;

        public GetAllFamiliesPaginationQueryHandler(IFamilyRepository familyRepository, IMapper mapper)
        {
            _familyRepository = familyRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<FamilyPaginationDto>> Handle(GetAllFamiliesPaginationQuery request, CancellationToken cancellationToken)
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

            var family = _familyRepository.All
                .Include(x => x.FamilyMembers)
                .ThenInclude(x => x.MemberUser)
                .Include(x => x.Debtors)
                .Include(x => x.Address)
                .Include(x => x.FamilyNotes)
                .Select(x => new FamilySimpleDTO
                {
                    MemberId = x.MemberId,
                    ReferenceNumber = x.ReferenceNumber,
                    Id = x.Id,
                    Name = x.FamilyMembers.Where(x => x.MemberTypeId == 1).FirstOrDefault().MemberUser.FirstName + " " + x.FamilyMembers.Where(x => x.MemberTypeId == 1).FirstOrDefault().MemberUser.LastName,
                    MemberCount = x.FamilyMembers.Count(X => X.MemberUser.IsDead == false || X.MemberUser.IsDeleted == false),
                    TotalDebtorAmount = x.Debtors.Where(x => x.IsPayment == false).Sum(x => x.Amount),
                    UserId = x.UserId,
                    Nationality = x.User.Nationality,
                    GenderId = x.User.GenderId,
                    Personnummer = x.User.IdentificationNumber,
                    DateOfBirth = x.User.BirthDay.Value.Day.ToString() + "." + x.User.BirthDay.Value.Month.ToString() + "." + x.User.BirthDay.Value.Year.ToString(),
                    IsActive = x.IsActive,
                    IsDeleted = x.IsDeleted,
                    CreationDate = x.CreationDate.Value.Day.ToString() + "." + x.CreationDate.Value.Month.ToString() + "." + x.CreationDate.Value.Year.ToString(),
                });

            if (request.MemberId != 0)
            {
                family = family.Where(x => x.MemberId.ToString().StartsWith(request.MemberId.ToString()));
            }
            if (request.Search != null)
            {
                family = family.Where(x => x.MemberId.ToString().ToUpper().StartsWith(request.Search.ToString().ToUpper()) || x.ReferenceNumber.ToString().ToUpper().StartsWith(request.Search.ToString().ToUpper()) || x.Name.ToUpper().StartsWith(request.Search.ToString().ToUpper()) || x.Personnummer.ToUpper().StartsWith(request.Search.ToString().ToUpper()) || x.CreationDate.StartsWith(request.Search) || x.DateOfBirth.StartsWith(request.Search));

                //family = family.Where(x => x.MemberId.ToString().Contains(request.Search.ToString()) && x.IsActive == request.IsActive && x.IsDeleted == request.IsDeleted) || (x.ReferenceNumber.ToString().Contains(request.Search.ToString()) && x.IsActive == request.IsActive && x.IsDeleted == request.IsDeleted);

            }
            if (request.IsActive != null)
            {
                family = family.Where(x => x.IsActive == request.IsActive);
            }
            if (request.IsDeleted != null)
            {
                family = family.Where(X => X.IsDeleted == request.IsDeleted);
            }
            if (!string.IsNullOrEmpty(request.OrderBy))
            {
                family = family.OrderBy(request.OrderBy);
            }
            else { family = family.OrderBy(x => x.MemberId); }

            var paginationData = family.AsEnumerable().Skip(request.Skip * request.PageSize).Take(request.PageSize).ToList();

            var list = new FamilyPaginationDto
            {
                Data = _mapper.Map<List<FamilySimpleDTO>>(paginationData),
                Skip = request.Skip,
                TotalCount = family.Count(),
                PageSize = request.PageSize,
            };
            return ServiceResponse<FamilyPaginationDto>.ReturnResultWith200(list);
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
