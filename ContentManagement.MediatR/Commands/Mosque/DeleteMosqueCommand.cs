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
    public class DeleteMosqueCommand : IRequest<ServiceResponse<MosqueDTO>>
    {
        public int Id { get; set; }
    }
}
