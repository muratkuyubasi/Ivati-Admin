using AutoMapper;
using ContentManagement.Common.UnitOfWork;
using ContentManagement.Data;
using ContentManagement.Data.Dto;
using ContentManagement.Domain;
using ContentManagement.Helper;
using ContentManagement.MediatR.Queries;
using ContentManagement.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ContentManagement.MediatR.Handlers
{
    public class GetDeletedFamilyMembersQueryHandler : IRequestHandler<GetDeletedFamilyMembersQuery, ServiceResponse<DeletedFamilyMemberWithFamilyPaginationDTO>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IFamilyMemberRepository _familyMemberRepository;
        private readonly IFamilyRepository _familyRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<PTContext> _uow;

        public GetDeletedFamilyMembersQueryHandler(IUserRepository userRepository, IMapper mapper, IFamilyMemberRepository familyMemberRepository, IUnitOfWork<PTContext> uow, IFamilyRepository familyRepository)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _familyMemberRepository = familyMemberRepository;
            _uow = uow;
            _familyRepository = familyRepository;
        }

        public async Task<ServiceResponse<DeletedFamilyMemberWithFamilyPaginationDTO>> Handle(GetDeletedFamilyMembersQuery request, CancellationToken cancellationToken)
        {
            // Generic Repo ile 
            //var list = _userRepository.All.ToList();
            //var deletedUsers = _userRepository.AllDelete(x=>x.IsDeleted == true).Include(x=>x.Family).Include(x=>x.Gender).ToList();

            DateTime date;
            var deletedUsers = _familyMemberRepository.All.Include(x => x.MemberUser).Include(x => x.Family).ThenInclude(x => x.Address).Select(x => new DeletedFamilyMemberWithFamilyDTO
            {
                Id = x.Id,
                MemberId = x.Family.MemberId,
                MemberUserId = x.MemberUser.Id,
                BirthDate = x.MemberUser.BirthDay.Value.Day.ToString() + "." + x.MemberUser.BirthDay.Value.Month.ToString() + "." + x.MemberUser.BirthDay.Value.Year.ToString(),
                FullName = x.MemberUser.FirstName + " " + x.MemberUser.LastName,
                GenderId = x.MemberUser.GenderId,
                Personummer = x.MemberUser.IdentificationNumber,
                CaddeVeSokak = x.Family.Address.Street,
                Il = x.Family.Address.District,
                IsDeleted = x.MemberUser.IsDeleted,
                Phone = x.MemberUser.PhoneNumber,
                Email = x.MemberUser.Email
            }).Where(x => x.IsDeleted == true).AsQueryable();

            if (request.Search != null)
            {
                deletedUsers = deletedUsers.Where(x =>
                        x.FullName.ToUpper().StartsWith(request.Search.ToUpper()) ||
                        x.Personummer.StartsWith(request.Search) ||
                        x.CaddeVeSokak.ToUpper().StartsWith(request.Search.ToUpper()) ||
                        x.Il.ToUpper().StartsWith(request.Search.ToUpper()) ||
                        x.Phone.ToUpper().StartsWith(request.Search.ToUpper()) ||
                        x.BirthDate.StartsWith(request.Search) ||
                        x.MemberId.ToString().StartsWith(request.Search));
            }
            if (request.OrderBy != null)
            {
                deletedUsers = deletedUsers.OrderBy(request.OrderBy);
            }
            else { deletedUsers = deletedUsers.OrderBy(X => X.MemberId); }
            //var deletedUsers = _familyMemberRepository.All/*.AllIncluding(x => x.Family)*/.Where(x=>x.IsDeleted==true).ToList();
            //var deletedUsers = _userRepository.FindBy(x=>x.FirstName == "Boran"/* && x.IsDeleted*/).FirstOrDefault();
            //var deletedUsers = _uow.Context.Users.Where(x => x.IsDeleted).ToList();
            //.Select(x => new FamilyMemberDTO
            //{
            //    IsDeleted = x.IsDeleted,
            //    MemberUser = x.MemberUser,
            //}).ToList();
            var paginationData = await deletedUsers.Skip(request.Skip * request.PageSize).Take(request.PageSize).OrderBy(x => x.MemberId).ToListAsync();

            var list = new DeletedFamilyMemberWithFamilyPaginationDTO
            {
                Data = _mapper.Map<List<DeletedFamilyMemberWithFamilyDTO>>(paginationData),
                Skip = request.Skip,
                TotalCount = deletedUsers.Count(),
                PageSize = request.PageSize,
            };
            return ServiceResponse<DeletedFamilyMemberWithFamilyPaginationDTO>.ReturnResultWith200(list);
        }
    }
}
