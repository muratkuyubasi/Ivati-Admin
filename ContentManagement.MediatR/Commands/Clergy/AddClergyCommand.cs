﻿using ContentManagement.Data.Dto;
using ContentManagement.Helper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentManagement.MediatR.Commands
{
    public class AddClergyCommand: IRequest<ServiceResponse<ClergyDTO>>
    {
        public string Image { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string JobDescription { get; set; }
        public string PlaceOfDuty { get; set; }
    }
}
