using ContentManagement.Common.GenericRespository;
using ContentManagement.Data.Models;
using ContentManagement.Helper;
using Microsoft.AspNetCore.Http.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentManagement.Repository
{
    public interface IAddressRepository : IGenericRepository<Address>
    {
    }
}
