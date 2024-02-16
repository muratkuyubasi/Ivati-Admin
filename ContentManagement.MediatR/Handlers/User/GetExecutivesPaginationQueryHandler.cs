using AutoMapper;
using ContentManagement.Data.Dto;
using ContentManagement.Helper;
using ContentManagement.MediatR.Queries;
using ContentManagement.Repository;
using DocumentFormat.OpenXml.Spreadsheet;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ContentManagement.MediatR.Handlers
{
    public class GetExecutivesPaginationQueryHandler : IRequestHandler<GetExecutivesPaginationQuery, ServiceResponse<UserPaginationDTO>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetExecutivesPaginationQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<UserPaginationDTO>> Handle(GetExecutivesPaginationQuery request, CancellationToken cancellationToken)
        {
            var executives = _userRepository.All.Include(x => x.UserRoles).Include(X=>X.Family).Where(x => x.UserRoles.Any(X => X.RoleId == Guid.Parse("0EE85745-F1B7-4F2B-92CC-595BF10A1BBB")))
                .Select(x => new UserSimpleDTO
                {
                    Id = x.Id,
                    FullName = x.FirstName + " " + x.LastName,
                    Email = x.Email,
                    IsActive = x.IsActive,
                    IsDeleted = x.IsDeleted,
                    Phone = x.PhoneNumber,
                    CityId = x.CityId,
                    FamilyId = x.Family.MemberId,
                    PicturePath = x.ProfilePhoto,
                    CreationDate = x.CreatedDate.Day.ToString() + "." + x.CreatedDate.Month.ToString() + "." + x.CreatedDate.Year.ToString(),
                });

            if (request.Search != null)
            {
                executives = executives.Where(x => x.FamilyId.ToString().StartsWith(request.Search.ToString())
                || x.FullName.ToUpper().StartsWith(request.Search.ToString().ToUpper())
                || x.CreationDate.StartsWith(request.Search.ToString())
                || x.Phone.StartsWith(request.Search.ToString())
                || x.Email.StartsWith(request.Search.ToString()));
            }
            if (request.IsActive != null)
            {
                executives = executives.Where(x => x.IsActive == request.IsActive);
            }
            if (request.IsDeleted != null)
            {
                executives = executives.Where(X => X.IsDeleted == request.IsDeleted);
            }
            if (request.CityId != 0)
            {
                executives = executives.Where(x => x.CityId == request.CityId);
            }
            if (!string.IsNullOrEmpty(request.OrderBy))
            {
                executives = executives.OrderBy(request.OrderBy);
            }
            else { executives = executives.OrderBy(x => x.FamilyId); }

            var paginationData = executives.AsEnumerable().Skip(request.Skip * request.PageSize).Take(request.PageSize).ToList();

            var list = new UserPaginationDTO
            {
                Data = _mapper.Map<List<UserSimpleDTO>>(paginationData),
                Skip = request.Skip,
                TotalCount = executives.Count(),
                PageSize = request.PageSize,
            };
            return ServiceResponse<UserPaginationDTO>.ReturnResultWith200(list);
        }
    }
}
