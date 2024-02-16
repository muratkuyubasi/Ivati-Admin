using AutoMapper;
using ContentManagement.Data.Dto;
using ContentManagement.Helper;
using ContentManagement.MediatR.Queries.User;
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

namespace ContentManagement.MediatR.Handlers
{
    public class GetAllUsersPaginationQueryHandler : IRequestHandler<GetAllUsersPaginationQuery, ServiceResponse<UserPaginationDTO>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetAllUsersPaginationQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }
        public async Task<ServiceResponse<UserPaginationDTO>> Handle(GetAllUsersPaginationQuery request, CancellationToken cancellationToken)
        {
            var users = _userRepository.All
                .Include(x => x.Family)
                .Include(X=>X.UserRoles)
                .Select(x => new UserSimpleDTO
                {
                    Id = x.Id,
                    FamilyId = x.Family.MemberId,
                    PicturePath = x.ProfilePhoto,
                    FullName = x.FirstName + " " + x.LastName,
                    IsActive = x.IsActive,
                    IsDeleted = x.IsDeleted,
                    CityId = (int)x.CityId,
                    CreationDate = x.CreatedDate.Day.ToString() + "." + x.CreatedDate.Month.ToString() + "." + x.CreatedDate.Year.ToString(),
                    Phone = x.PhoneNumber,
                    Email = x.Email,
                    RoleId = x.UserRoles.Where(X=>X.UserId == x.Id).FirstOrDefault().RoleId
                });

            if (request.Search != null)
            {
                users = users.Where(x => x.FamilyId.ToString().StartsWith(request.Search.ToString()) 
                || x.FullName.ToUpper().StartsWith(request.Search.ToString().ToUpper())
                || x.CreationDate.StartsWith(request.Search.ToString())
                || x.Phone.StartsWith(request.Search.ToString())
                || x.Email.StartsWith(request.Search.ToString()));
            }
            if (request.IsActive != null)
            {
                users = users.Where(x => x.IsActive == request.IsActive);
            }
            if (request.IsDeleted != null)
            {
                users = users.Where(X => X.IsDeleted == request.IsDeleted);
            }
            if (request.CityId != 0)
            {
                users = users.Where(x => x.CityId == request.CityId);
            }
            if (request.RoleId !=0)
            {
                if (request.RoleId == 1) // Üst Yöneticiler
                {
                    users = users.Where(x => x.RoleId == Guid.Parse("0EE85745-F1B7-4F2B-92CC-595BF10A1BBB")); //Buraya üst yönetici rolünün idsi gelecek
                }
                if (request.RoleId == 2) // Yöneticiler
                {
                    users = users.Where(x => x.RoleId == Guid.Parse("0EE85745-F1B7-4F2B-92CC-595BF10A1BBB")); //Buraya yönetici rolünün idsi gelecek
                }
                //Başka roller çekilmek istenirse buraya eklenir
            }
            if (!string.IsNullOrEmpty(request.OrderBy))
            {
                users = users.OrderBy(request.OrderBy);
            }
            else { users = users.OrderBy(x => x.FamilyId); }

            var paginationData = users.AsEnumerable().Skip(request.Skip * request.PageSize).Take(request.PageSize).ToList();

            var list = new UserPaginationDTO
            {
                Data = _mapper.Map<List<UserSimpleDTO>>(paginationData),
                Skip = request.Skip,
                TotalCount = users.Count(),
                PageSize = request.PageSize,
            };
            return ServiceResponse<UserPaginationDTO>.ReturnResultWith200(list);
        }
    }
}
