using ContentManagement.Data.Dto;
using ContentManagement.DataDto;
using ContentManagement.Helper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentManagement.MediatR.Commands
{
    public class DeleteNewsCommand : IRequest<ServiceResponse<NewsDTO>>
    {
        public int Id { get; set; }
    }
}
