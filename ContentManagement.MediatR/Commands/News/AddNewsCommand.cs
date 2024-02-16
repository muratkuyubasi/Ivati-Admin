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
    public class AddNewsCommand: IRequest<ServiceResponse<NewsDTO>>
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        [DefaultValue(false)]
        public bool IsActive { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
