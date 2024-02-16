using ContentManagement.Common.UnitOfWork;
using ContentManagement.Data;
using ContentManagement.Domain;
using ContentManagement.Helper;
using ContentManagement.MediatR.Commands.User;
using ContentManagement.Repository;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ContentManagement.MediatR.Handlers
{
    public class ResetAllUsersPasswordCommandHandler : IRequestHandler<ResetAllUsersPasswordCommand, ServiceResponse<bool>>
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork<PTContext> _unitOfWork;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IRoleRepository _roleRepository;

        public ResetAllUsersPasswordCommandHandler(IUserRepository userRepository, UserManager<User> userManager, IUnitOfWork<PTContext> unitOfWork, IUserRoleRepository userRoleRepository, IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _userRoleRepository = userRoleRepository;
            _roleRepository = roleRepository;
        }
        public async Task<ServiceResponse<bool>> Handle(ResetAllUsersPasswordCommand request, CancellationToken cancellationToken)
        {
            if (request.Approve == "1")
            {
                var adminrole = _roleRepository.All.Where(x => x.Name == "Super Admin").FirstOrDefault();
                var users = await _userRepository.All.Where(x => x.IsActive == true && x.UserName != null && !x.UserRoles.Any(x => x.RoleId == adminrole.Id)).ToListAsync();
                foreach (var user in users)
                {
                    var activerole = _userRoleRepository.FindBy(x => x.UserId == user.Id && x.RoleId == Guid.Parse("0EE85745-F1B7-4F2B-92CC-595BF10A1BBB")).FirstOrDefault();
                    if (activerole == null)
                    {
                        var role = new UserRole
                        {
                            RoleId = Guid.Parse("0EE85745-F1B7-4F2B-92CC-595BF10A1BBB"),
                            UserId = user.Id
                        };
                        _userRoleRepository.Add(role);
                    }
                    string code = await _userManager.GeneratePasswordResetTokenAsync(user);
                    if (user.IdentificationNumber != null)
                    {
                        string numara = user.IdentificationNumber;
                        string sifre = numara.Substring(0, numara.Length >= 6 ? 6 : numara.Length);
                        IdentityResult passwordResult2 =
                            await _userManager.ResetPasswordAsync(user, code, sifre);
                    }
                    else
                    {
                        IdentityResult passwordResult =
                            await _userManager.ResetPasswordAsync(user, code, "icfuser951753");
                    }
                    _userRepository.Update(user);
                }
                if (await _unitOfWork.SaveAsync() <= 0)
                {
                    return ServiceResponse<bool>.Return500();
                }
                else return ServiceResponse<bool>.ReturnSuccess();
            }
            else return ServiceResponse<bool>.Return409("Başlatılamadı!");
        }
    }
}
