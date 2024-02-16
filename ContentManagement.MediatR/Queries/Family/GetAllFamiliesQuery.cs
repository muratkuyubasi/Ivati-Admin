using ContentManagement.Data.Dto;
using ContentManagement.Helper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentManagement.MediatR.Queries
{
    public class GetAllFamiliesQuery : IRequest<ServiceResponse<List<FamilyDTO>>>
    {
        public int Distance { get; set; }
    }
}
