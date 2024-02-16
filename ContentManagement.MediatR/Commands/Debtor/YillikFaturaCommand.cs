using ContentManagement.Helper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentManagement.MediatR.Commands.Debtor
{
    public class YillikFaturaCommand : IRequest<ServiceResponse<bool>>
    {
        public string Durum { get; set; }
    }
}
