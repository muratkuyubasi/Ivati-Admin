﻿using CenazeFonu.Common.GenericRespository;
using CenazeFonu.Common.UnitOfWork;
using CenazeFonu.Data;
using CenazeFonu.Domain;

namespace CenazeFonu.Repository
{
    public class FrontGalleryMediaRepository : GenericRepository<FrontGalleryMedia, PTContext>,
          IFrontGalleryMediaRepository
    {
        public FrontGalleryMediaRepository(
            IUnitOfWork<PTContext> uow
            ) : base(uow)
        {
        }
    }
}