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
    public class UpdateDebtorCommand : IRequest<ServiceResponse<DebtorDTO>>
    {
        public string DebtorNumber { get; set; }
        public Guid FamilyId { get; set; }
    }
}
