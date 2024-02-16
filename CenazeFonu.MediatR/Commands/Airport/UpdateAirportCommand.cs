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
    public class UpdateAirportCommand: IRequest<ServiceResponse<AirportDTO>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
