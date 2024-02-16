using CenazeFonu.Data.Models;
using CenazeFonu.Helper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CenazeFonu.MediatR.Commands
{
    public class AddMemberTypeCommand : IRequest<ServiceResponse<MemberTypeDTO>>
    {
        public string Name { get; set; }
    }
}
