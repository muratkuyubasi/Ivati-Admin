﻿using CenazeFonu.Common.GenericRespository;
using CenazeFonu.Common.UnitOfWork;
using CenazeFonu.Data;
using CenazeFonu.Data.Models;
using CenazeFonu.Domain;

namespace CenazeFonu.Repository
{
    public class ServiceRepository : GenericRepository<Services, PTContext>,
          IServiceRepository
    {
        public ServiceRepository(
            IUnitOfWork<PTContext> uow
            ) : base(uow)
        {
        }
    }
}