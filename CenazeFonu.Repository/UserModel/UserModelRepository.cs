﻿using CenazeFonu.Common.GenericRespository;
using CenazeFonu.Common.UnitOfWork;
using CenazeFonu.Data.Models;
using CenazeFonu.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CenazeFonu.Repository
{
    public class UserModelRepository : GenericRepository<UserModel, PTContext>, IUserModelRepository
    {
        public UserModelRepository(
           IUnitOfWork<PTContext> uow
           ) : base(uow)
        {
        }
    }
}