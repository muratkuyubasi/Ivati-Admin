using ContentManagement.Data;
using ContentManagement.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection.Emit;

namespace ContentManagement.Domain
{
    public class PTContext : IdentityDbContext<User, Role, Guid, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        public PTContext(DbContextOptions options) : base(options)
        {
            this.Database.SetCommandTimeout(999999);            
        }
        public override DbSet<User> Users { get; set; }
        public override DbSet<Role> Roles { get; set; }
        public override DbSet<UserClaim> UserClaims { get; set; }
        public override DbSet<UserRole> UserRoles { get; set; }
        public override DbSet<UserLogin> UserLogins { get; set; }
        public override DbSet<RoleClaim> RoleClaims { get; set; }
        public override DbSet<UserToken> UserTokens { get; set; }
        public DbSet<ContentManagement.Data.Action> Actions { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<PageAction> PageActions { get; set; }
        public DbSet<NLog> NLog { get; set; }
        public DbSet<LoginAudit> LoginAudits { get; set; }
        public DbSet<AppSetting> AppSettings { get; set; }
        public DbSet<UserAllowedIP> UserAllowedIPs { get; set; }
        public DbSet<EmailTemplate> EmailTemplates { get; set; }
        public DbSet<EmailSMTPSetting> EmailSMTPSettings { get; set; }

        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Age> Ages { get; set; }
        public virtual DbSet<Citizenship> Citizenships { get; set; }
        public virtual DbSet<Debtor> Debtors { get; set; }
        public virtual DbSet<Family> Families { get; set; }
        public virtual DbSet<FamilyMember> FamilyMembers { get; set; }
        public virtual DbSet<UserDetail> UserDetails { get; set; }

        public virtual DbSet<UserModel> UserModel { get; set; }

        public virtual DbSet<Spouse> Spouses { get; set; }

        public virtual DbSet<Data.Models.Gender> Genders { get; set; }
        public virtual DbSet<MaritalStatus> MaritalStatuses { get; set; }
        public virtual DbSet<RoomType> RoomTypes { get; set; }

        public virtual DbSet<Association> Associations { get; set; }

        public virtual DbSet<UmreForm> UmreForms { get; set; }

        public virtual DbSet<HacForm> HacForms { get; set; }

        public virtual DbSet<Airport> Airports { get; set; }

        public virtual DbSet<News> News { get; set; }

        public virtual DbSet<Mosque> Mosques { get; set; }

        public virtual DbSet<FoundationPublication> FoundationPublications { get; set; }

        public virtual DbSet<Clergy> Clergies { get; set; }

        public virtual DbSet<Activity> Activities { get; set; }
        public virtual DbSet<Services> Services { get; set; }

        public virtual DbSet<Country> Countries { get; set; }

        public virtual DbSet<City> Cities { get; set; }

        public virtual DbSet<Note> Notes { get; set; }

        public virtual DbSet<MemberType> MemberTypes { get; set; }

        public virtual DbSet<ReplacementId> ReplacementIds { get; set; }

        public virtual DbSet<DebtorType> DebtorTypes { get; set; }

        public virtual DbSet<FamilyNote> FamilyNotes { get; set; }

        public virtual DbSet<HacPeriod> HacPeriods { get; set; }

        public virtual DbSet<UmrePeriod> UmrePeriods { get; set; }

        public virtual DbSet<Language> Languages { get; set; }

        public virtual DbSet<Translation> Translations { get; set; }

        public virtual DbSet<Chairman> Chairmen { get; set; }

        public DbSet<AtaseModel> Atases { get; set; }

        public virtual DbSet<Executive> Executives { get; set; }

        public virtual DbSet<Project> Projects { get; set; }

        //FRONT
        public DbSet<FrontMenu> FrontMenus { get; set; }
        public DbSet<FrontMenuRecord> FrontMenuRecords { get; set; }
        public DbSet<FrontPage> FrontPages { get; set; }
        public DbSet<FrontPageRecord> FrontPageRecords { get; set; }
        public DbSet<FrontAnnouncement> FrontAnnouncements { get; set; }
        public DbSet<FrontAnnouncementRecord> FrontAnnouncementRecords { get; set; }
        public DbSet<FrontGallery> FrontGalleries { get; set; }
        public DbSet<FrontGalleryRecord> FrontGalleryRecords { get; set; }
        public DbSet<FrontGalleryMedia> FrontGalleryMedias { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            //builder.Entity<Family>().Navigation(f => f.FamilyMembers).AutoInclude();
            //builder.Entity<FamilyMember>().Navigation(fm => fm.MemberUser).AutoInclude();
            //builder.Entity<FamilyMember>().Navigation(fm => fm.Family).AutoInclude();
            //builder.Entity<Debtor>().Navigation(d => d.DebtorType).AutoInclude();
            ////builder.Entity<Debtor>().Navigation(d => d.Family).AutoInclude();
            //builder.Entity<Family>().Navigation(fn => fn.FamilyNotes).AutoInclude();
            //builder.Entity<Family>().Navigation(d=>d.Debtors).AutoInclude();
            //builder.Entity<Family>().Navigation(a=>a.Address).AutoInclude();

            builder.Entity<Executive>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Executives)
                    .HasForeignKey(d => d.CityId)
                    .HasConstraintName("FK_Executives_Cities");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Executives)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Executives_Roles");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Executives)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Executives_Users");
            });

            builder.Entity<Language>(entity =>
            {
                entity.Property(e => e.Langcode)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            builder.Entity<Translation>(entity =>
            {
                entity.Property(e => e.Author).HasMaxLength(250);

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description).HasMaxLength(250);

                entity.Property(e => e.Image).HasMaxLength(250);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.HasOne(d => d.Language)
                    .WithMany(p => p.Translations)
                    .HasForeignKey(d => d.LanguageId)
                    .HasConstraintName("FK_Translations_Languages");
            });

            builder.Entity<Project>(entity =>
            {
                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description).HasMaxLength(250);

                entity.Property(e => e.Image).HasMaxLength(250);

                entity.Property(e => e.LinkImage).HasMaxLength(250);

                entity.Property(e => e.LinkUrl).HasMaxLength(250);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.HasOne(d => d.Language)
                    .WithMany(p => p.Projects)
                    .HasForeignKey(d => d.LanguageId)
                    .HasConstraintName("FK_Projects_Languages");
            });

            builder.Entity<ReplacementId>(entity =>
            {
                entity.Property(x => x.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });
            builder.Entity<Note>(entity =>
            {
                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Text).IsRequired();

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Notes)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Notes_Notes");
            });

            builder.Entity<FamilyNote>(entity =>
            {
                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Text).IsRequired();

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.HasOne(d => d.Family)
                    .WithMany(p => p.FamilyNotes)
                    .HasForeignKey(d => d.FamilyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FamilyNote_Family");
            });

            builder.Entity<City>(entity =>
            {
                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Cities)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UmreForms_Association");

                entity.Property(e => e.CreationDate).HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            builder.Entity<Country>(entity =>
            {
                entity.Property(e => e.CreationDate).HasColumnType("datetime")
                   .HasDefaultValueSql("(getdate())");
            });

            builder.Entity<Association>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            builder.Entity<Airport>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            builder.Entity<Data.Models.Gender>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(6);
            });

            builder.Entity<News>(entity =>
            {
                entity.Property(e => e.CreationDate).HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            builder.Entity<MaritalStatus>(entity =>
            {
                entity.ToTable("MaritalStatus");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(7);
            });

            builder.Entity<RoomType>(entity =>
            {
                entity.Property(e => e.RoomTypes)
                    .IsRequired()
                    .HasMaxLength(15);
            });

            builder.Entity<UmreForm>(entity =>
            {
                entity.HasIndex(e => e.ClosestAssociationId, "IX_UmreForms_ClosestAssociationId");

                entity.HasIndex(e => e.GenderId, "IX_UmreForms_GenderId");

                entity.HasIndex(e => e.MaritalStatusId, "IX_UmreForms_MaritalStatusId");

                entity.HasIndex(e => e.RoomTypeId, "IX_UmreForms_RoomTypeId");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Country)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.Explanation)
                    .HasMaxLength(200);

                entity.Property(e => e.FatherName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.HeadshotPicture)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.MotherName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Nationality)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PassportExpirationDate).HasColumnType("date");

                entity.Property(e => e.PassportGivenDate).HasColumnType("date");

                entity.Property(e => e.PassportGivenPlace)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PassportNumber)
                    .IsRequired()
                    .HasMaxLength(9);

                entity.Property(e => e.PassportPicture)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.PlaceOfBirth)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.TurkeyIdentificationNumber)
                    .IsRequired()
                    .HasMaxLength(11);

                entity.HasOne(d => d.ClosestAssociation)
                    .WithMany(p => p.UmreForms)
                    .HasForeignKey(d => d.ClosestAssociationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UmreForms_Association");

                entity.HasOne(d => d.DepartureAirport)
                    .WithMany(p => p.UmreFormDepartureAirports)
                    .HasForeignKey(d => d.DepartureAirportId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UmreForms_DepartureAirport");

                entity.HasOne(d => d.Gender)
                    .WithMany(p => p.UmreForms)
                    .HasForeignKey(d => d.GenderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UmreForms_Gender");

                entity.HasOne(d => d.LandingAirport)
                    .WithMany(p => p.UmreFormLandingAirports)
                    .HasForeignKey(d => d.LandingAirportId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UmreForms_LandingAirport");

                entity.HasOne(d => d.MaritalStatus)
                    .WithMany(p => p.UmreForms)
                    .HasForeignKey(d => d.MaritalStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UmreForms_MaritalStatus");

                entity.HasOne(d => d.RoomType)
                    .WithMany(p => p.UmreForms)
                    .HasForeignKey(d => d.RoomTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UmreForms_RoomType");

                entity.HasOne(d => d.Period)
                    .WithMany(p => p.UmreForms)
                    .HasForeignKey(d => d.PeriodId)
                    .HasConstraintName("FK_UmreForms_UmrePeriod");
            });

            builder.Entity<HacForm>(entity =>
            {
                entity.HasIndex(e => e.ClosestAssociationId, "IX_HacForms_ClosestAssociationId");

                entity.HasIndex(e => e.GenderId, "IX_HacForms_GenderId");

                entity.HasIndex(e => e.MaritalStatusId, "IX_HacForms_MaritalStatusId");

                entity.HasIndex(e => e.RoomTypeId, "IX_HacForms_RoomTypeId");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Country)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.Explanation)
                    .HasMaxLength(200);

                entity.Property(e => e.FatherName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.HeadshotPicture)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.MotherName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Nationality)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PassportExpirationDate).HasColumnType("date");

                entity.Property(e => e.PassportGivenDate).HasColumnType("date");

                entity.Property(e => e.PassportGivenPlace)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.PassportNumber)
                    .IsRequired()
                    .HasMaxLength(9);

                entity.Property(e => e.PassportPicture)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.PlaceOfBirth)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.TurkeyIdentificationNumber)
                    .IsRequired()
                    .HasMaxLength(11);

                entity.HasOne(d => d.ClosestAssociation)
                    .WithMany(p => p.HacForms)
                    .HasForeignKey(d => d.ClosestAssociationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HacForms_Association");

                entity.HasOne(d => d.DepartureAirport)
                    .WithMany(p => p.HacFormDepartureAirports)
                    .HasForeignKey(d => d.DepartureAirportId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HacForms_DepartureAirport");

                entity.HasOne(d => d.Gender)
                    .WithMany(p => p.HacForms)
                    .HasForeignKey(d => d.GenderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HacForms_Gender");

                entity.HasOne(d => d.LandingAirport)
                    .WithMany(p => p.HacFormLandingAirports)
                    .HasForeignKey(d => d.LandingAirportId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HacForms_LandingAirport");

                entity.HasOne(d => d.MaritalStatus)
                    .WithMany(p => p.HacForms)
                    .HasForeignKey(d => d.MaritalStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HacForms_MaritalStatus");

                entity.HasOne(d => d.RoomType)
                    .WithMany(p => p.HacForms)
                    .HasForeignKey(d => d.RoomTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HacForms_RoomType");

                entity.HasOne(d => d.Period)
                    .WithMany(p => p.HacForms)
                    .HasForeignKey(d => d.PeriodId)
                    .HasConstraintName("FK_HacForms_HacPeriods");
            });

            builder.Entity<HacPeriod>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150);
            });
            builder.Entity<UmrePeriod>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150);
            });

            builder.Entity<User>(b =>
            {
                // Each User can have many UserClaims
                b.HasMany(e => e.UserClaims)
                    .WithOne(e => e.User)
                    .HasForeignKey(uc => uc.UserId)
                    .IsRequired();

                // Each User can have many UserLogins
                b.HasMany(e => e.UserLogins)
                    .WithOne(e => e.User)
                    .HasForeignKey(ul => ul.UserId)
                    .IsRequired();

                // Each User can have many UserTokens
                b.HasMany(e => e.UserTokens)
                    .WithOne(e => e.User)
                    .HasForeignKey(ut => ut.UserId)
                    .IsRequired();

                // Each User can have many entries in the UserRole join table
                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.User)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();

                b.HasOne(d => d.Gender)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.GenderId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

            });

            builder.Entity<Spouse>(entity =>
            {
                entity.HasOne(d => d.Family)
                    .WithMany(p => p.Spouses)
                    .HasForeignKey(d => d.FamilyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Spouses_FamilyMember");

                entity.HasOne(d => d.Husband)
                    .WithMany(p => p.Spouses)
                    .HasForeignKey(d => d.HusbandId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Spouses_Users");

                entity.HasOne(d => d.Gender)
                    .WithMany(p => p.Spouse)
                    .HasForeignKey(d => d.GenderId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.SpouseUsers)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Spouses_Spouse_User");
            });

            builder.Entity<Role>(b =>
            {
                // Each Role can have many entries in the UserRole join table
                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.Role)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                // Each Role can have many associated RoleClaims
                b.HasMany(e => e.RoleClaims)
                    .WithOne(e => e.Role)
                    .HasForeignKey(rc => rc.RoleId)
                    .IsRequired();

                b.HasOne(e => e.CreatedByUser)
                    .WithMany()
                    .HasForeignKey(ur => ur.CreatedBy)
                    .OnDelete(DeleteBehavior.NoAction);

                b.HasOne(e => e.ModifiedByUser)
                    .WithMany()
                    .HasForeignKey(rc => rc.ModifiedBy)
                    .OnDelete(DeleteBehavior.NoAction);

                b.HasOne(e => e.DeletedByUser)
                    .WithMany()
                    .HasForeignKey(rc => rc.DeletedBy)
                    .OnDelete(DeleteBehavior.NoAction);

            });

            builder.Entity<Data.Action>(b =>
            {
                b.HasOne(e => e.CreatedByUser)
                    .WithMany()
                    .HasForeignKey(ur => ur.CreatedBy)
                    .OnDelete(DeleteBehavior.NoAction);

                b.HasOne(e => e.ModifiedByUser)
                    .WithMany()
                    .HasForeignKey(rc => rc.ModifiedBy)
                    .OnDelete(DeleteBehavior.NoAction);

                b.HasOne(e => e.DeletedByUser)
                    .WithMany()
                    .HasForeignKey(rc => rc.DeletedBy)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            builder.Entity<PageAction>(b =>
            {
                b.HasOne(e => e.CreatedByUser)
                    .WithMany()
                    .HasForeignKey(ur => ur.CreatedBy)
                    .OnDelete(DeleteBehavior.NoAction);

                b.HasOne(e => e.ModifiedByUser)
                    .WithMany()
                    .HasForeignKey(rc => rc.ModifiedBy)
                    .OnDelete(DeleteBehavior.NoAction);

                b.HasOne(e => e.DeletedByUser)
                    .WithMany()
                    .HasForeignKey(rc => rc.DeletedBy)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            builder.Entity<Page>(b =>
            {
                b.HasOne(e => e.CreatedByUser)
                    .WithMany()
                    .HasForeignKey(ur => ur.CreatedBy)
                    .OnDelete(DeleteBehavior.NoAction);

                b.HasOne(e => e.ModifiedByUser)
                    .WithMany()
                    .HasForeignKey(rc => rc.ModifiedBy)
                    .OnDelete(DeleteBehavior.NoAction);

                b.HasOne(e => e.DeletedByUser)
                    .WithMany()
                    .HasForeignKey(rc => rc.DeletedBy)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            builder.Entity<UserAllowedIP>().HasKey(c => new { c.UserId, c.IPAddress });

            builder.Entity<EmailSMTPSetting>(b =>
            {
                b.HasOne(e => e.CreatedByUser)
                    .WithMany()
                    .HasForeignKey(ur => ur.CreatedBy)
                    .OnDelete(DeleteBehavior.NoAction);

                b.HasOne(e => e.ModifiedByUser)
                    .WithMany()
                    .HasForeignKey(rc => rc.ModifiedBy)
                    .OnDelete(DeleteBehavior.NoAction);

                b.HasOne(e => e.DeletedByUser)
                    .WithMany()
                    .HasForeignKey(rc => rc.DeletedBy)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            builder.Entity<Address>(entity =>
            {
                entity.HasKey(e => e.FamilyId);

                entity.Property(e => e.FamilyId).ValueGeneratedNever();

                entity.Property(e => e.District)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.PostCode)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Street)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Addresses)
                    .HasForeignKey(d => d.CityId)
                    .HasConstraintName("FK_Addresses_Cities");

                entity.HasOne(d => d.Family)
                    .WithOne(p => p.Address)
                    .HasForeignKey<Address>(d => d.FamilyId)
                    .HasConstraintName("FK_Addresses_Families");
            });

            builder.Entity<Age>(entity =>
            {
                //entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");

                //entity.Property(e => e.Name)
                //    .IsRequired()
                //    .HasMaxLength(50);
            });

            builder.Entity<Citizenship>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            builder.Entity<Debtor>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Family)
                    .WithMany(p => p.Debtors)
                    .HasForeignKey(d => d.FamilyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Debtors_Families");

                entity.HasOne(d => d.DebtorType)
                    .WithMany(p => p.Debtors)
                    .HasForeignKey(d => d.DebtorTypeId)
                    .HasConstraintName("FK_Debtors_DebtorTypes");
            });

            builder.Entity<DebtorType>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            builder.Entity<Family>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(x => x.MemberId).UseIdentityColumn();

                //entity.HasOne(d => d.User)
                //    .WithMany(p => p.Families)
                //    .HasForeignKey(d => d.UserId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_Families_Users");
            });

            builder.Entity<FamilyMember>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.Family)
                    .WithMany(p => p.FamilyMembers)
                    .HasForeignKey(d => d.FamilyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FamilyMembers_Families");

                entity.HasOne(d => d.MemberType)
                    .WithMany(p => p.FamilyMembers)
                    .HasForeignKey(d => d.MemberTypeId)
                    .HasConstraintName("FK_FamilyMembers_MemberType");

                entity.HasOne(d => d.MemberUser)
                    .WithMany(p => p.FamilyMembers)
                    .HasForeignKey(d => d.MemberUserId);
            });

            builder.Entity<MemberType>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(1000);
            });

            //modelBuilder.Entity<User>(entity =>
            //{
            //    entity.Property(e => e.Id).ValueGeneratedNever();

            //    entity.Property(e => e.Email).HasMaxLength(256);

            //    entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

            //    entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

            //    entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

            //    entity.Property(e => e.UserName).HasMaxLength(256);
            //});

            builder.Entity<UserDetail>(entity =>
            {
                entity.Property(e => e.IdentificationNumber)
                    .IsRequired()
                    .HasMaxLength(11);

                entity.HasOne(d => d.CitizenShip)
                    .WithMany(p => p.UserDetails)
                    .HasForeignKey(d => d.CitizenShipId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserDetails_Citizenship");

                //entity.HasOne(d => d.User)
                //    .WithMany(p => p.UserDetails)
                //    .HasForeignKey(d => d.UserId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_UserDetails_Users");
            });

            builder.Entity<User>().ToTable("Users");
            builder.Entity<Role>().ToTable("Roles");
            builder.Entity<RoleClaim>().ToTable("RoleClaims");
            builder.Entity<UserClaim>().ToTable("UserClaims");
            builder.Entity<UserLogin>().ToTable("UserLogins");
            builder.Entity<UserRole>().ToTable("UserRoles");
            builder.Entity<UserToken>().ToTable("UserTokens");
            builder.DefalutMappingValue();
            builder.DefalutDeleteValueFilter();
        }
    }
}
