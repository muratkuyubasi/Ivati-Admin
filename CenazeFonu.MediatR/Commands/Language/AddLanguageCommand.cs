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
    public class AddLanguageCommand: IRequest<ServiceResponse<LanguageDTO>>
    {
        public string Name { get; set; }
        public string Langcode { get; set; }
        public string? Flag { get; set; }
    }
}