using ContentManagement.Data.Dto;
using ContentManagement.Helper;
using ContentManagement.MediatR.Queries;
using ContentManagement.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ContentManagement.MediatR.Handlers
{
    public class GetExecutiveDetailByUserIDQueryHandler : IRequestHandler<GetExecutiveDetailByUserIDQuery, ServiceResponse<ExecutiveUserDTO>>
    {
        private readonly IUserRepository _userRepository;

        public GetExecutiveDetailByUserIDQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<ServiceResponse<ExecutiveUserDTO>> Handle(GetExecutiveDetailByUserIDQuery request, CancellationToken cancellationToken)
        {
            var executives = _userRepository.AllIncluding(X => X.Executives, x => x.Family, x=>x.UserRoles)
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
        RoleId = user.UserRoles.Where(x=>x.UserId == user.Id).FirstOrDefault().RoleId,
        Cities = user.Executives.Select(executive => new CitySimpleDTO
        {
            Id = (int)executive.CityId,
            Name = executive.City.Name,
        }).ToList()
    }).FirstOrDefault();
            if (executives == null)
            {
                return ServiceResponse<ExecutiveUserDTO>.Return404("Bu ID'ye ait bir kayıt bulunamadı!");
            }
            else return ServiceResponse<ExecutiveUserDTO>.ReturnResultWith200(executives);
        }
    }
}
