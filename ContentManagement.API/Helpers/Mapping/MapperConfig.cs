using AutoMapper;

namespace ContentManagement.API.Helpers.Mapping
{
    public static class MapperConfig
    {
        public static IMapper GetMapperConfigs()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ActionProfile());
                mc.AddProfile(new PageProfile());
                mc.AddProfile(new RoleProfile());
                mc.AddProfile(new UserProfile());
                mc.AddProfile(new PageActionProfile());
                mc.AddProfile(new AppSettingProfile());
                mc.AddProfile(new NLogProfile());
                mc.AddProfile(new EmailTemplateProfile());
                mc.AddProfile(new EmailProfile());
                mc.AddProfile(new AddressProfile());
                mc.AddProfile(new AgeProfile());
                mc.AddProfile(new CitizenshipProfile());
                mc.AddProfile(new DebtorProfile());
                mc.AddProfile(new FamilyMemberProfile());
                mc.AddProfile(new FamilyProfile());
                mc.AddProfile(new UserDetailProfile());
                mc.AddProfile(new UserModelProfile());
                mc.AddProfile(new SpouseProfile());
                mc.AddProfile(new AssociationProfile());
                mc.AddProfile(new UmreFormProfile());
                mc.AddProfile(new GenderProfile());
                mc.AddProfile(new MaritalStatusProfile());
                mc.AddProfile(new RoomTypeProfile());
                mc.AddProfile(new HacProfile());
                mc.AddProfile(new AirportProfile());
                mc.AddProfile(new FrontProfile());
                mc.AddProfile(new ClergyProfile());
                mc.AddProfile(new FoundationPublicationProfile());
                mc.AddProfile(new MosqueProfile());
                mc.AddProfile(new NewsProfile());
                mc.AddProfile(new ActivityProfile());
                mc.AddProfile(new ServiceProfile());
                mc.AddProfile(new CityProfile());
                mc.AddProfile(new CountryProfile());
                mc.AddProfile(new NoteProfile());
                mc.AddProfile(new MemberTypeProfile());
                mc.AddProfile(new ReplacementIdProfile());
                mc.AddProfile(new DebtorTypeProfile());
                mc.AddProfile(new FamilyNoteProfile());
                mc.AddProfile(new AtaseProfile());
                mc.AddProfile(new ChairmanProfile());
                mc.AddProfile(new LanguageProfile());
                mc.AddProfile(new TranslationProfile());
                mc.AddProfile(new ExecutiveProfile());
                mc.AddProfile(new ProjectProfile());
            });
            return mappingConfig.CreateMapper();
        }
    }
}
