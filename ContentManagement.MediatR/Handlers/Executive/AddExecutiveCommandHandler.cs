using AutoMapper;
using ContentManagement.Common.UnitOfWork;
using ContentManagement.Data;
using ContentManagement.Data.Dto;
using ContentManagement.Data.Models;
using ContentManagement.Domain;
using ContentManagement.Helper;
using ContentManagement.MediatR.Commands;
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
    public class AddExecutiveCommandHandler : IRequestHandler<AddExecutiveCommand, ServiceResponse<ExecutiveDTO>>
    {
        private readonly IExecutiveRepository _executiveRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IUnitOfWork<PTContext> _uow;
        private readonly IMapper _mapper;

        public AddExecutiveCommandHandler(IExecutiveRepository executiveRepository, IUserRoleRepository userRoleRepository, IRoleRepository roleRepository, IUserRepository userRepository, IUnitOfWork<PTContext> uow, IMapper mapper)
        {
            _executiveRepository = executiveRepository;
            _userRoleRepository = userRoleRepository;
            _roleRepository = roleRepository;
            _userRepository = userRepository;
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<ExecutiveDTO>> Handle(AddExecutiveCommand request, CancellationToken cancellationToken)
        {
            var user = _userRepository.FindBy(x => x.Id == request.UserId).FirstOrDefault();
            if (user == null)
            {
                return ServiceResponse<ExecutiveDTO>.Return404("Bu ID'ye ait bir kullanıcı bulunamadı!");
            }
            var role = _roleRepository.FindBy(x => x.Id == request.RoleId).FirstOrDefault();
            if (role == null)
            {
                return ServiceResponse<ExecutiveDTO>.Return404("Bu ID'ye ait bir rol bulunamadı!");
            }
            var executive = new Executive();
            foreach(var city in request.CityId)
            {
                executive = new Executive
                {
                    Id = Guid.NewGuid(),
                    UserId = user.Id,
                    RoleId = role.Id,
                    CityId = city.Value
                };
                _executiveRepository.Add(executive);
            }
            var us = new UserRole
            {
                UserId = user.Id,
                RoleId = role.Id,
            };
            _userRoleRepository.Add(us);
            if (await _uow.SaveAsync()<0)
            {
                return ServiceResponse<ExecutiveDTO>.Return409("Kayıt işlemi sırasında bir hata meydana geldi!");
            }
            return ServiceResponse<ExecutiveDTO>.ReturnResultWith200(_mapper.Map<ExecutiveDTO>(executive));
        }
    }
}
