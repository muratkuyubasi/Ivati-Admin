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
    public class AddFoundationPublicationCommand: IRequest<ServiceResponse<FoundationPublicationDTO>>
    {
        public string Image { get; set; }
        public string Name { get; set; }
        public string Subtitle { get; set; }
        public string Description { get; set; }
        public string CompiledBy { get; set; }
        public string Translator { get; set; }
        public string RequestNumber { get; set; }

        public DateTime PublicationDate { get; set; }
    }
}
