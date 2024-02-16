﻿using CenazeFonu.Common.GenericRespository;
using CenazeFonu.Data.Models;
using CenazeFonu.Data.Resources;
using CenazeFonu.Helper;
using Microsoft.AspNetCore.Http.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CenazeFonu.Repository
{
    public interface IFamilyRepository : IGenericRepository<Family>
    {
        Task<FamilyList> GetFamilies(FamilyResource familyResource);
    }
}