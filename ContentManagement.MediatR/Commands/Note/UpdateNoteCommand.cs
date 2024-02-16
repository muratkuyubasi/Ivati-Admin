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
    public class UpdateNoteCommand : IRequest<ServiceResponse<NoteDTO>>
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public string Text { get; set; }
        public DateTime? UpdateDate { get; set; }

        public bool isDeleted { get; set; }

    }
}
