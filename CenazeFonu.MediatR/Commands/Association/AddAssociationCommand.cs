using CenazeFonu.Data.Dto;
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
    public class AddAssociationCommand : IRequest<ServiceResponse<AssociationDTO>>
    {
        public string Name { get; set; }
    }
}
