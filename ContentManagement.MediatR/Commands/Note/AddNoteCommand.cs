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
    public class AddNoteCommand : IRequest<ServiceResponse<NoteDTO>>
    {
        public Guid UserId { get; set; }
        public string Text { get; set; }

        public DateTime? CreationDate { get; set; }
    }
}
