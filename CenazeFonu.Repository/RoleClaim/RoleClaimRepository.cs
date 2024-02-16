﻿using CenazeFonu.Common.GenericRespository;
using CenazeFonu.Common.UnitOfWork;
using CenazeFonu.Data;
using CenazeFonu.Domain;

namespace CenazeFonu.Repository
{
    public class RoleClaimRepository : GenericRepository<RoleClaim, PTContext>,
           IRoleClaimRepository
    {
        public RoleClaimRepository(
            IUnitOfWork<PTContext> uow
            ) : base(uow)
        {
        }
    }
}