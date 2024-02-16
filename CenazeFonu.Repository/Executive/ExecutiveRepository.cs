﻿using CenazeFonu.Common.GenericRespository;
using CenazeFonu.Common.UnitOfWork;
using CenazeFonu.Data;
using CenazeFonu.Data.Models;
using CenazeFonu.Domain;

namespace CenazeFonu.Repository
{
    public class ExecutiveRepository : GenericRepository<Executive, PTContext>,
          IExecutiveRepository
    {
        public ExecutiveRepository(
            IUnitOfWork<PTContext> uow
            ) : base(uow)
        {
        }
    }
}