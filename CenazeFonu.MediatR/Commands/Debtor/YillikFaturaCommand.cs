using CenazeFonu.Helper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CenazeFonu.MediatR.Commands.Debtor
{
    public class YillikFaturaCommand : IRequest<ServiceResponse<bool>>
    {
        public string Durum { get; set; }
    }
}
