using ContentManagement.Data;
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
    public class UpdateChairmanCommand : IRequest<ServiceResponse<ChairmanDTO>>
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string Title { get; set; }

        public string? Description { get; set; }

        public string? Image { get; set; }
    }
}
