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
    public class UpdateServicesCommand : IRequest<ServiceResponse<ServicesDTO>>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}