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
    public class DeleteDebtorCommand : IRequest<ServiceResponse<DebtorDTO>>
    {
        public Guid FamilyId { get; set; }

        public string DebtorNumber { get; set; }
    }
}
