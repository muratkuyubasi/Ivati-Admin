using CenazeFonu.Common.GenericRespository;
using CenazeFonu.Common.UnitOfWork;
using CenazeFonu.Data.Dto;
using CenazeFonu.Data.Models;
using CenazeFonu.Data.Resources;
using CenazeFonu.Domain;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CenazeFonu.Repository
{
    public class FamilyRepository : GenericRepository<Family, PTContext>,
          IFamilyRepository
    {
        private readonly IPropertyMappingService _propertyMappingService;
        public FamilyRepository(
            IUnitOfWork<PTContext> uow
, IPropertyMappingService propertyMappingService) : base(uow)
        {
            _propertyMappingService = propertyMappingService;
        }

        public async Task<FamilyList> GetFamilies(FamilyResource familyResource)
        {
            var collectionBeforePaging = All;

                //.Include(x => x.FamilyMembers).ThenInclude(x => x.MemberUser)
                //.Include(x => x.Address).Include(x => x.Debtors).Include(x => x.FamilyNotes);
            collectionBeforePaging =
               collectionBeforePaging.ApplySort(familyResource.OrderBy,
               _propertyMappingService.GetPropertyMapping<FamilyDTO, Family>());

            var loginAudits = new FamilyList();
            return await loginAudits.Create(
                collectionBeforePaging,
                familyResource.Skip,
                familyResource.PageSize
                );
        }
    }
}
