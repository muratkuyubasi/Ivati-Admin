using ContentManagement.Helper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentManagement.MediatR.Commands
{
    public class PrintAllDebtorsToFileCommand : IRequest<ServiceResponse<string>>
    {
        public bool Approve { get; set; }

        public int Year { get; set; }
    }
}
