﻿using CenazeFonu.Data.Dto;
using CenazeFonu.Helper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CenazeFonu.MediatR.Commands
{
    public class UpdateFamilyNoteCommand : IRequest<ServiceResponse<FamilyNoteDTO>>
    {
        public int Id { get; set; }
        public Guid FamilyId { get; set; }
        public string Text { get; set; }
        public DateTime? UpdateDate { get; set; }

        public bool isDeleted { get; set; }

    }
}