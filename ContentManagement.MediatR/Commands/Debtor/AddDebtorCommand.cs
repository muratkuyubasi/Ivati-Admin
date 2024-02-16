using ContentManagement.Data.Dto;
using ContentManagement.Helper;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentManagement.MediatR.Commands
{
    public class AddDebtorCommand : IRequest<ServiceResponse<DebtorDTO>>
    {
        public Guid FamilyId { get; set; }
        public decimal Amount { get; set; }
        public DateTime? DueDate { get; set; }

        public int DebtorTypeId { get; set; }
    }
}
