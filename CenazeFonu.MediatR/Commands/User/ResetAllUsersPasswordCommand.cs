using CenazeFonu.Helper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CenazeFonu.MediatR.Commands.User
{
    public class ResetAllUsersPasswordCommand : IRequest<ServiceResponse<bool>>
    {
        public string Approve { get; set; }
    }
}
