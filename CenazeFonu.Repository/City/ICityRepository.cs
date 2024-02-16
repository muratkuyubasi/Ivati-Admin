using CenazeFonu.Common.GenericRespository;
using CenazeFonu.Data;
using CenazeFonu.Data.Models;
using CenazeFonu.Helper;
using Microsoft.AspNetCore.Http.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CenazeFonu.Repository
{
    public interface ICityRepository : IGenericRepository<City>
    {
    }
}
