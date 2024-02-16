﻿using CenazeFonu.Data.Dto;
using CenazeFonu.Data.Dto;
using CenazeFonu.Helper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CenazeFonu.MediatR.Commands
{
    public class AddProjectCommand: IRequest<ServiceResponse<ProjectDTO>>
    {
        public int? LanguageId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string? LinkImage { get; set; }
        public string? LinkUrl { get; set; }
    }
}