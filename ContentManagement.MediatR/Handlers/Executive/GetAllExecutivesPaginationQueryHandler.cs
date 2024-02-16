using AutoMapper;
using ContentManagement.Data.Dto;
using ContentManagement.Data.Models;
using ContentManagement.Helper;
using ContentManagement.MediatR.Queries;
using ContentManagement.Repository;
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
    public class GetAllExecutivesPaginationQueryHandler : IRequestHandler<GetAllExecutivesPaginationQuery, ServiceResponse<ExecutivePaginationDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetAllExecutivesPaginationQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<ExecutivePaginationDto>> Handle(GetAllExecutivesPaginationQuery request, CancellationToken cancellationToken)
        {
            var executives = _userRepository.AllIncluding(X => X.Executives, x => x.Family)
            .Where(X => X.Executives.Any(x => x.UserId == X.Id))
            .Select(user => new ExecutiveUserDTO
            {
                Id = user.Id,
                FamilyId = user.Family.MemberId,
                CreationDate = user.CreatedDate.Day.ToString() + "." + user.CreatedDate.Month.ToString() + "." + user.CreatedDate.Year.ToString(),
                PicturePath = user.ProfilePhoto,
                FullName = user.FirstName + " " + user.LastName,
                IsActive = user.IsActive,
                IsDeleted = user.IsDeleted,
                Phone = user.PhoneNumber,
                Email = user.Email,
                Address = user.Address,
                Cities = user.Executives.Select(executive => new CitySimpleDTO
                {
                    Id = (int)executive.CityId,
                    Name = executive.City.Name,
                }).ToList()
            }).AsNoTracking();

            if (request.Search != null)
            {
                executives = executives.Where(x => x.FamilyId.ToString().ToUpper().StartsWith(request.Search.ToString().ToUpper())
                || x.FullName.ToUpper().StartsWith(request.Search.ToString().ToUpper())
                || x.Email.ToUpper().StartsWith(request.Search.ToString().ToUpper())
                || x.Phone.ToUpper().StartsWith(request.Search.ToString().ToUpper())
                || x.CreationDate.StartsWith(request.Search));
            }
            if (request.IsActive != null)
            {
                executives = executives.Where(x => x.IsActive == request.IsActive);
            }
            if (request.IsDeleted != null)
            {
                executives = executives.Where(X => X.IsDeleted == request.IsDeleted);
            }
            if (!string.IsNullOrEmpty(request.OrderBy))
            {
                executives = executives.OrderBy(request.OrderBy);
            }
            else { executives = executives.OrderBy(x => x.FamilyId); }

            var paginationData = executives.AsEnumerable().Skip(request.Skip * request.PageSize).Take(request.PageSize).ToList();

            var list = new ExecutivePaginationDto
            {
                Data = _mapper.Map<List<ExecutiveUserDTO>>(paginationData),
                Skip = request.Skip,
                TotalCount = executives.Count(),
                PageSize = request.PageSize,
            };
            return ServiceResponse<ExecutivePaginationDto>.ReturnResultWith200(list);
        }
    }
}
