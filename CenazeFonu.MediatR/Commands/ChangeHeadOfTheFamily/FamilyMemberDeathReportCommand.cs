using CenazeFonu.Data.Dto;
using CenazeFonu.Helper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CenazeFonu.MediatR.Commands
{
    public class FamilyMemberDeathReportCommand : IRequest<ServiceResponse<UserInformationDTO>>
    {
        public Guid UserId { get; set; }
    }
}
