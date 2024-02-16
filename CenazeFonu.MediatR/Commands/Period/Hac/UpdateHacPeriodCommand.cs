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
    public class UpdateHacPeriodCommand : IRequest<ServiceResponse<HacPeriodDTO>>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public bool? IsActive { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? FinishDate { get; set; }
    }
}
