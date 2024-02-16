using ContentManagement.Data.Dto;
using ContentManagement.Helper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentManagement.MediatR.Commands
{
    public class UpdateUserInformationCommand : IRequest<ServiceResponse<UpdateUserContactDTO>>
    {
        //public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string PostCode { get; set; }
        public string District { get; set; }
        public string Street { get; set; }
        //public string UserName { get; set; }
        public string Password { get; set; }
    }
}
