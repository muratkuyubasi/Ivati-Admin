using ContentManagement.Common.GenericRespository;
using ContentManagement.Common.UnitOfWork;
using ContentManagement.Data.Dto;
using ContentManagement.Data.Models;
using ContentManagement.Data.Resources;
using ContentManagement.Domain;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ContentManagement.Repository
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
