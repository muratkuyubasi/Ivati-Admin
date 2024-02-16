using MediatR;
using Microsoft.EntityFrameworkCore;
using ContentManagement.MediatR.Queries;
using ContentManagement.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ContentManagement.MediatR.Handlers
{
    public class CheckUserQueryHandler : IRequestHandler<CheckUserQuery, bool>
    {
        private readonly IUserRepository _userRepository;
        public CheckUserQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public Task<bool> Handle(CheckUserQuery request, CancellationToken cancellationToken)
        {
            bool userChecked = false;
            var checkUser =  _userRepository.FindBy(x => x.UserName == request.UserName).FirstOrDefault();
            if(checkUser != null ){
                userChecked = false;
            }
            else { userChecked= true; }
            return Task.FromResult(userChecked);
        }
    }
}
