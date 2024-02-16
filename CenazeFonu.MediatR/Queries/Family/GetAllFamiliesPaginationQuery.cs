﻿using CenazeFonu.Data.Dto;
using CenazeFonu.Data.Resources;
using CenazeFonu.Helper;
using CenazeFonu.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CenazeFonu.MediatR.Queries
{
    public class GetAllFamiliesPaginationQuery : /*IRequest<FamilyList>*/ IRequest<ServiceResponse<FamilyPaginationDto>>
    {
        public int Skip { get; set; } = 0;
        public int PageSize { get; set; }

        public bool? IsActive { get; set; } = null;

        public bool? IsDeleted { get; set; } = null;

        public int MemberId { get; set; }

        public string? Search { get; set; }

        public string? OrderBy { get; set; }

        //public FamilyResource FamilyResource { get; set; }
    }
}