using AutoMapper;
using ContentManagement.Data.Dto;
using ContentManagement.Helper;
using ContentManagement.MediatR.Queries;
using ContentManagement.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ContentManagement.MediatR.Handlers
{
    public class GetExecutiveByIDQueryHandler : IRequestHandler<GetExecutiveByIDQuery, ServiceResponse<ExecutiveSingleUserDTO>>
    {
        private readonly IExecutiveRepository _executiveRepository;
        private readonly IMapper _mapper;

        public GetExecutiveByIDQueryHandler(IExecutiveRepository executiveRepository, IMapper mapper)
        {
            _executiveRepository = executiveRepository;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<ExecutiveSingleUserDTO>> Handle(GetExecutiveByIDQuery request, CancellationToken cancellationToken)
        {
            var data = _executiveRepository.All.Include(x => x.User).Include(x => x.City).Where(X => X.Id == request.Id).Select(x => new ExecutiveSingleUserDTO
            {
                Id = x.UserId,
                FamilyId = x.User.Family.MemberId,
                FullName = x.User.FirstName + " " + x.User.LastName,
                Email = x.User.Email,
                Phone = x.User.PhoneNumber,
                IsActive = x.User.IsActive,
                IsDeleted = x.User.IsDeleted,
                PicturePath = x.User.ProfilePhoto,
                RoleId = x.RoleId,
                CityId = x.CityId,
                CityName = x.City.Name,
                CreationDate = x.User.CreatedDate.Day.ToString() + "." + x.User.CreatedDate.Month.ToString() + "." + x.User.CreatedDate.Year.ToString(),
            }).FirstOrDefault();
            if (data == null)
            {
                return ServiceResponse<ExecutiveSingleUserDTO>.Return404("Bu ID'ye ait bir kayıt bulunamadı!");
            }
            else return ServiceResponse<ExecutiveSingleUserDTO>.ReturnResultWith200(data);
        }
    }
}
