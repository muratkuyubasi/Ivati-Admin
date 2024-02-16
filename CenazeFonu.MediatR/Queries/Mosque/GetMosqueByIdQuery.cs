﻿using CenazeFonu.Data.Dto;
using CenazeFonu.Helper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CenazeFonu.MediatR.Queries
{
    public class GetMosqueByIdQuery : IRequest<ServiceResponse<MosqueDTO>>
    {
        public int Id { get; set; }
    }
}