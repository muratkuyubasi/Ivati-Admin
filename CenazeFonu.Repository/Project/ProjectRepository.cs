using CenazeFonu.Data;
using CenazeFonu.Common.GenericRespository;
using CenazeFonu.Common.UnitOfWork;
using CenazeFonu.Data;
using CenazeFonu.Data.Models;
using CenazeFonu.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CenazeFonu.Repository
{
    public class ProjectRepository : GenericRepository<Project, PTContext>, IProjectRepository
    {
        public ProjectRepository(
           IUnitOfWork<PTContext> uow
           ) : base(uow)
        {
        }
    }
}
