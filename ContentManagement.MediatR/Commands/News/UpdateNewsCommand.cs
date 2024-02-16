using ContentManagement.Data.Dto;
using ContentManagement.DataDto;
using ContentManagement.Helper;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentManagement.MediatR.Commands
{
    public class UpdateNewsCommand: IRequest<ServiceResponse<NewsDTO>>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        [DefaultValue(false)]
        public bool IsActive { get; set; }
        public string? UpdatedBy { get; set; }
        public string CreatedBy { get; set; }

        public DateTime CreationDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
