using CenazeFonu.Data.Dto;
using CenazeFonu.Helper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CenazeFonu.MediatR.Queries
{
    public class GetAllFamiliesQuery : IRequest<ServiceResponse<List<FamilyDTO>>>
    {
        public int Distance { get; set; }
    }
}
