using ContentManagement.Data.Dto;
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
    public class DeleteAtaseCommand : IRequest<ServiceResponse<AtaseModelDTO>>
    {
        public int Id { get; set; }
    }
}
