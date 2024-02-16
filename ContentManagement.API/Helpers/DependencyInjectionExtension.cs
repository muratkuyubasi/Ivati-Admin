using ContentManagement.Common.UnitOfWork;
using ContentManagement.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace ContentManagement.API.Helpers
{
    public static class DependencyInjectionExtension
    {
        public static void AddDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
            services.AddScoped<IPropertyMappingService, PropertyMappingService>();
            services.AddScoped<IPageRepository, PageRepository>();
            services.AddScoped<IActionRepository, ActionRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            services.AddScoped<IUserClaimRepository, UserClaimRepository>();
            services.AddScoped<IRoleClaimRepository, RoleClaimRepository>();
            services.AddScoped<IPageActionRepository, PageActionRepository>();
            services.AddScoped<ILoginAuditRepository, LoginAuditRepository>();
            services.AddScoped<IUserAllowedIPRepository, UserAllowedIPRepository>();
            services.AddScoped<IAppSettingRepository, AppSettingRepository>();
            services.AddScoped<INLogRespository, NLogRespository>();
            services.AddScoped<IEmailTemplateRepository, EmailTemplateRepository>();
            services.AddScoped<IEmailSMTPSettingRepository, EmailSMTPSettingRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<IAgeRepository, AgeRepository>();
            services.AddScoped<ICitizenshipRepository, CitizenshipRepository>();
            services.AddScoped<IDebtorRepository, DebtorRepository>();
            services.AddScoped<IFamilyRepository, FamilyRepository>();
            services.AddScoped<IFamilyMemberRepository, FamilyMemberRepository>();
            services.AddScoped<IUserDetailRepository, UserDetailRepository>();
            services.AddScoped<IUserModelRepository, UserModelRepository>();
            services.AddScoped<ISpouseRepository, SpouseRepository>();
            services.AddScoped<IUmreFormRepository, UmreFormRepository>();
            services.AddScoped<IAssociationRepository, AssociationRepository>();
            services.AddScoped<IHacRepository, HacFormRepository>();
            services.AddScoped<IGenderRepository, GenderRepository>();
            services.AddScoped<IRoomTypeRepository, RoomTypeRepository>();
            services.AddScoped<IAirportRepository, AirportRepository>();
            services.AddScoped<IClergyRepository, ClergyRepository>();
            services.AddScoped<IFoundationPublicationRepository, FoundationPublicationRepository>();
            services.AddScoped<IMosqueRepository, MosqueRepository>();
            services.AddScoped<INewsRepository, NewsRepository>();
            services.AddScoped<IFrontAnnouncementRepository, FrontAnnouncementRepository>();
            services.AddScoped<IFrontAnnouncementRecordRepository, FrontAnnouncementRecordRepository>();
            services.AddScoped<IFrontGalleryRepository, FrontGalleryRepository>();
            services.AddScoped<IFrontGalleryMediaRepository, FrontGalleryMediaRepository>();
            services.AddScoped<IFrontGalleryRecordRepository, FrontGalleryRecordRepository>();
            services.AddScoped<IFrontMenuRepository, FrontMenuRepository>();
            services.AddScoped<IFrontMenuRecordRepository, FrontMenuRecordRepository>();
            services.AddScoped<IFrontPageRepository, FrontPageRepository>();
            services.AddScoped<IFrontPageRecordRepository, FrontPageRecordRepository>();
            services.AddScoped<IActivityRepository, ActivityRepository>();
            services.AddScoped<IServiceRepository, ServiceRepository>();
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<INoteRepository, NoteRepository>();
            services.AddScoped<IMemberTypeRepository, MemberTypeRepository>();
            services.AddScoped<IReplacementIdRepository, ReplacementIdRepository>();
            services.AddScoped<IDebtorTypeRepository, DebtorTypeRepository>();
            services.AddScoped<IFamilyNoteRepository, FamilyNoteRepository>();
            services.AddScoped<IHacPeriodRepository, HacPeriodRepository>();
            services.AddScoped<IUmrePeriodRepository, UmrePeriodRepository>();
            services.AddScoped<IAtaseRepository, AtaseRepository>();
            services.AddScoped<ILanguageRepository, LanguageRepository>();
            services.AddScoped<ITranslationRepository, TranslationRepository>();
            services.AddScoped<IChairmanRepository, ChairmanRepository>();
            services.AddScoped<IExecutiveRepository, ExecutiveRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
        }
    }
}
