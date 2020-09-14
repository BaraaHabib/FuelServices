using FuelServices.DBContext.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DBContext.Models
{
    public class AirportCoreContext : IdentityDbContext<ApplicationUser,ApplicationRole,string>
    {
        public AirportCoreContext(DbContextOptions<AirportCoreContext> options)
            : base(options)
        {

        }

        public virtual DbSet<Advertisement> Advertisement { get; set; }
        public virtual DbSet<AdvertisementCategory> AdvertisementCategory { get; set; }
        public virtual DbSet<AdvertisementOwner> AdvertisementOwner { get; set; }
        public virtual DbSet<AdvertisementProperty> AdvertisementProperty { get; set; }
        public virtual DbSet<AdvertisementType> AdvertisementType { get; set; }
        public virtual DbSet<AdvertisementTypeProperty> AdvertisementTypeProperty { get; set; }
        public virtual DbSet<Airport> Airport { get; set; }
        public virtual DbSet<AirportProperty> AirportProperty { get; set; }
        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<Contact> Contact { get; set; }
        public virtual DbSet<ContactUs> ContactUs { get; set; }
        public virtual DbSet<ContentManagement> ContentManagement { get; set; }
        public virtual DbSet<Continent> Continent { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<CustomerPackagesLog> CustomerPackagesLog { get; set; }
        public virtual DbSet<SupplierPackagesLog> SupplierPackagesLog { get; set; }
        public virtual DbSet<EmailBodyDefaultParams> EmailBodyDefaultParams { get; set; }
        public virtual DbSet<Feature> Feature { get; set; }
        public virtual DbSet<FuelSupplier> FuelSupplier { get; set; }
        public virtual DbSet<FuelType> FuelType { get; set; }
        public virtual DbSet<Log> Log { get; set; }
        public virtual DbSet<Offer> Offer { get; set; }
        public virtual DbSet<OfferFuelType> OfferFuelType { get; set; }
        public virtual DbSet<AirportOffer> AirportOffers { get; set; }
        public virtual DbSet<OfferPartyExcludes> OfferPartyExcludes { get; set; }
        public virtual DbSet<OfferProperties> OfferProperties { get; set; }
        public virtual DbSet<PaymentPackage> PaymentPackage { get; set; }
        public virtual DbSet<PaymentPackageFeature> PaymentPackageFeature { get; set; }
        public virtual DbSet<Property> Property { get; set; }
        public virtual DbSet<Request> Request { get; set; }
        public virtual DbSet<RequestOffers> RequestOffers { get; set; }
        public virtual DbSet<SupplierContact> SupplierContact { get; set; }
        public virtual DbSet<SupplierContactPerson> SupplierContactPerson { get; set; }
        public virtual DbSet<SupplierContactPersonContact> SupplierContactPersonContact { get; set; }
        public virtual DbSet<SupplierProperties> SupplierProperties { get; set; }
        public virtual DbSet<SupplierReview> SupplierReview { get; set; }
        public virtual DbSet<SupplierType> SupplierType { get; set; }
        public virtual DbSet<ResetPasswordToken> ResetPasswordTokens { get; set; }
        public virtual DbSet<AirportAds> AirportAds { get; set; }
        public virtual DbSet<AdvertisementImageType> AdvertisementImageTypes { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseLazyLoadingProxies(true);
        //    }
        //}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<ApplicationUser>(entity =>
            {
                entity.Property(e => e.IsActive).HasDefaultValue<bool>(true);
            });

            modelBuilder.Entity<Advertisement>(entity =>
            {
                entity.HasIndex(e => e.AdvertisementCategoryId);

                entity.HasIndex(e => e.AdvertisementOwnerId);

                entity.HasIndex(e => e.AdvertisementTypeId);

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.ImageUrl).IsRequired();

                entity.Property(e => e.PublishDate).HasColumnType("datetime");

                entity.HasOne(d => d.AdvertisementCategory)
                    .WithMany(p => p.Advertisement)
                    .HasForeignKey(d => d.AdvertisementCategoryId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_Advertisement_AdvertisementCategory");

                entity.HasOne(d => d.AdvertisementOwner)
                    .WithMany(p => p.Advertisement)
                    .HasForeignKey(d => d.AdvertisementOwnerId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_Advertisment_AdvertismentOwner");

                entity.HasOne(d => d.AdvertisementType)
                    .WithMany(p => p.Advertisement)
                    .HasForeignKey(d => d.AdvertisementTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Advertisment_AdvertismentType");
            });

            modelBuilder.Entity<AdvertisementCategory>(entity =>
            {
                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<AdvertisementOwner>(entity =>
            {
                entity.HasIndex(e => e.CountryId);

                entity.HasIndex(e => e.UserId)
                    .IsUnique()
                    .HasFilter("([UserId] IS NOT NULL)");

                entity.Property(e => e.Email).IsRequired();

                entity.Property(e => e.FirstName).IsRequired();

                entity.Property(e => e.LastName).IsRequired();

                entity.Property(e => e.Phone).IsRequired();

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.AdvertisementOwner)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_AdvertisementOwner_Country");

                entity.HasOne(d => d.User)
                    .WithOne(p => p.AdvertisementOwner)
                    .HasForeignKey<AdvertisementOwner>(d => d.UserId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_AdvertisementOwner_User");
            });

            modelBuilder.Entity<AdvertisementProperty>(entity =>
            {
                entity.HasIndex(e => e.AdvertisementId);

                entity.HasIndex(e => e.AdvertisementTypePropertyId);

                entity.Property(e => e.Value).IsRequired();

                entity.HasOne(d => d.Advertisement)
                    .WithMany(p => p.AdvertisementProperty)
                    .HasForeignKey(d => d.AdvertisementId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AdvertismentProperties_Advertisment");

                entity.HasOne(d => d.AdvertisementTypeProperty)
                    .WithMany(p => p.AdvertisementProperty)
                    .HasForeignKey(d => d.AdvertisementTypePropertyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AdvertismentProperties_AdvertismentTypeProperty");
            });

            modelBuilder.Entity<AdvertisementType>(entity =>
            {
                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<AdvertisementTypeProperty>(entity =>
            {
                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<Airport>(entity =>
            {
                entity.HasIndex(e => e.CityId);

                entity.HasIndex(e => e.CountryId);

                entity.HasIndex(e => e.NearestCityId);

                entity.Property(e => e.Iata).HasColumnName("IATA");

                entity.Property(e => e.Icao).HasColumnName("ICAO");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.AirportCity)
                    .HasForeignKey(d => d.CityId)
                    .HasConstraintName("FK_Airport_City");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Airport)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_Airport_Country");

                entity.HasOne(d => d.NearestCity)
                    .WithMany(p => p.AirportNearestCity)
                    .HasForeignKey(d => d.NearestCityId)
                    .HasConstraintName("FK_Airport_City1");
            });

            modelBuilder.Entity<AirportProperty>(entity =>
            {
                entity.HasIndex(e => e.AirportId);

                entity.HasIndex(e => e.PropertyId);

                entity.Property(e => e.Value).IsRequired();

                entity.HasOne(d => d.Airport)
                    .WithMany(p => p.AirportProperty)
                    .HasForeignKey(d => d.AirportId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AirportPorperties_Airports");

                entity.HasOne(d => d.Property)
                    .WithMany(p => p.AirportProperty)
                    .HasForeignKey(d => d.PropertyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AirportPorperties_Property");
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.HasIndex(e => e.CountryId);

                entity.Property(e => e.Name).IsRequired();

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.City)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_City_Country");
            });

            modelBuilder.Entity<Contact>(entity =>
            {
                entity.Property(e => e.DisplayName).IsRequired();

                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<ContactUs>(entity =>
            {
                entity.HasIndex(e => e.CustomerId);

                entity.Property(e => e.Email).IsRequired();

                entity.Property(e => e.FirstName).IsRequired();

                entity.Property(e => e.LastName).IsRequired();

                entity.Property(e => e.Message).IsRequired();

                entity.Property(e => e.Subject).IsRequired();

                entity.Property(e => e.SubmitDate).HasColumnType("datetime");

                entity.Property(e => e.Tel).IsRequired();

                entity.Property(e => e.IsRead).HasDefaultValue<bool>(false);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.ContactUs)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_ContactUs_Customer");
            });

            modelBuilder.Entity<ContentManagement>(entity =>
            {
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.DisplayName).IsRequired();

            });

            modelBuilder.Entity<Continent>(entity =>
            {
                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.HasIndex(e => e.ContinentId);

                entity.HasOne(d => d.Continent)
                    .WithMany(p => p.Country)
                    .HasForeignKey(d => d.ContinentId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_Country_Continent");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasIndex(e => e.CountryId);

                entity.HasIndex(e => e.UserId)
                    .IsUnique()
                    .HasFilter("([UserId] IS NOT NULL)");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Customer)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_Customer_Country");

                entity.HasOne(d => d.User)
                    .WithOne(p => p.Customer)
                    .HasForeignKey<Customer>(d => d.UserId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_Customer_User");
            });

            modelBuilder.Entity<CustomerPackagesLog>(entity =>
            {
                entity.HasIndex(e => e.CityId);

                entity.HasIndex(e => e.CountryId);

                entity.HasIndex(e => e.CustomerId);

                entity.HasIndex(e => e.PaymentPackageId);

                entity.Property(e => e.Address1).IsRequired();

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.Phone1).IsRequired();

                entity.Property(e => e.PostalCode).IsRequired();

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.CustomerPackagesLog)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_CustomerPackagesLog_Customer");

                entity.HasOne(d => d.PaymentPackage)
                    .WithMany(p => p.CustomerPackagesLogs)
                    .HasForeignKey(d => d.PaymentPackageId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_CustomerPackagesLog_PaymentPackage");
            });
            modelBuilder.Entity<SupplierPackagesLog>(entity =>
            {
                entity.HasIndex(e => e.CityId);

                entity.HasIndex(e => e.CountryId);

                entity.HasIndex(e => e.FuelSupplierId);

                entity.HasIndex(e => e.PaymentPackageId);

                entity.Property(e => e.Address1).IsRequired();

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.Phone1).IsRequired();

                entity.Property(e => e.PostalCode).IsRequired();

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.HasOne(d => d.FuelSupplier)
                    .WithMany(p => p.SupplierPackagesLog)
                    .HasForeignKey(d => d.FuelSupplierId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_FuelSupplierPackagesLog_Customer");

                entity.HasOne(d => d.PaymentPackage)
                    .WithMany(p => p.SupplierPackagesLogs)
                    .HasForeignKey(d => d.PaymentPackageId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_FuelSupplierPackagesLog_PaymentPackage");
            });

            modelBuilder.Entity<Feature>(entity =>
            {
                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<FuelSupplier>(entity =>
            {
                entity.HasIndex(e => e.CountryId);

                entity.HasIndex(e => e.UserId)
                    .IsUnique()
                    .HasFilter("([UserId] IS NOT NULL)");

                entity.Property(e => e.Name).IsRequired();

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.FuelSupplier)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_Supplier_Country");

                entity.HasOne(d => d.User)
                    .WithOne(p => p.FuelSupplier)
                    .HasForeignKey<FuelSupplier>(d => d.UserId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_Supplier_AspNetUsers");
            });

            modelBuilder.Entity<FuelType>(entity =>
            {
                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<Log>(entity =>
            {
                entity.Property(e => e.Level).HasMaxLength(128);

                entity.Property(e => e.Properties).HasColumnType("xml");
            });

            modelBuilder.Entity<Offer>(entity =>
            {
                entity.HasIndex(e => e.FuelSupplierId);

                entity.Property(e => e.DuesTaxesLevies).HasColumnName("DuesTaxesLevies");

                entity.HasOne(d => d.FuelSupplier)
                    .WithMany(p => p.Offer)
                    .HasForeignKey(d => d.FuelSupplierId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<OfferFuelType>(entity =>
            {
                entity.HasIndex(e => e.FuelTypeId);

                entity.HasIndex(e => e.OfferId);

                entity.HasOne(d => d.FuelType)
                    .WithMany(p => p.OfferFuelType)
                    .HasForeignKey(d => d.FuelTypeId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.Offer)
                    .WithMany(p => p.OfferFuelType)
                    .HasForeignKey(d => d.OfferId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<AirportOffer>(entity =>
            {
                entity.HasIndex(e => e.AirportId);

                entity.HasIndex(e => e.CityId);



                entity.HasIndex(e => e.OfferId);

                entity.HasIndex(e => e.SupplierTypeId);

                entity.HasOne(d => d.Airport)
                    .WithMany(p => p.AirportOffer)
                    .HasForeignKey(d => d.AirportId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.AirportOffer)
                    .HasForeignKey(d => d.CityId);


                entity.HasOne(d => d.Offer)
                    .WithMany(p => p.AirportOffers)
                    .HasForeignKey(d => d.OfferId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.SupplierType)
                    .WithMany(p => p.AirportOffer)
                    .HasForeignKey(d => d.SupplierTypeId);
            });

            modelBuilder.Entity<OfferPartyExcludes>(entity =>
            {
                entity.HasIndex(e => e.AirportId);

                entity.HasIndex(e => e.CityId);

                entity.HasIndex(e => e.CountryId);

                entity.HasIndex(e => e.AirportOfferId);

                entity.HasOne(d => d.Airport)
                    .WithMany(p => p.OfferPartyExcludes)
                    .HasForeignKey(d => d.AirportId);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.OfferPartyExcludes)
                    .HasForeignKey(d => d.CityId);

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.OfferPartyExcludes)
                    .HasForeignKey(d => d.CountryId);

                entity.HasOne(d => d.AirportOffer)
                    .WithMany(p => p.OfferPartyExcludes)
                    .HasForeignKey(d => d.AirportOfferId);
            });

            modelBuilder.Entity<OfferProperties>(entity =>
            {
                entity.HasIndex(e => e.OfferId);

                entity.HasIndex(e => e.PropertyId);

                entity.HasOne(d => d.Offer)
                    .WithMany(p => p.OfferProperties)
                    .HasForeignKey(d => d.OfferId)
                    .HasConstraintName("FK_OfferProperties_Offer");

                entity.HasOne(d => d.Property)
                    .WithMany(p => p.OfferProperties)
                    .HasForeignKey(d => d.PropertyId)
                    .HasConstraintName("FK_OfferProperties_Property");
            });

            modelBuilder.Entity<PaymentPackage>(entity =>
            {
                entity.Property(e => e.DiscountUnit).HasMaxLength(256);

                entity.Property(e => e.DisplayName).IsRequired();

                entity.Property(e => e.MainColor)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.PriceUnit)
                    .IsRequired()
                    .HasMaxLength(256);
            });

            modelBuilder.Entity<PaymentPackageFeature>(entity =>
            {
                entity.HasIndex(e => e.FeatureId);

                entity.HasIndex(e => e.PaymentPackageId);

                entity.Property(e => e.Value).IsRequired();

                entity.HasOne(d => d.Feature)
                    .WithMany(p => p.PaymentPackageFeature)
                    .HasForeignKey(d => d.FeatureId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PaymentPackageFeature_PaymentPackage");

                entity.HasOne(d => d.PaymentPackage)
                    .WithMany(p => p.PaymentPackageFeature)
                    .HasForeignKey(d => d.PaymentPackageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PaymentPackageFeature_Feature");
            });

            modelBuilder.Entity<Property>(entity =>
            {
                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<Request>(entity =>
            {
                entity.HasIndex(e => e.AirportId);

                entity.HasIndex(e => e.CustomerId);

                //entity.HasIndex(e => e.OfferFuelTypeId);


                entity.Property(e => e.AircraftType).IsRequired();

                entity.Property(e => e.ArrivalDate).HasDefaultValueSql("('0001-01-01T00:00:00.0000000')");

                entity.Property(e => e.CallSign).IsRequired();

                entity.Property(e => e.DepartureDate).HasDefaultValueSql("('0001-01-01T00:00:00.0000000')");

                entity.Property(e => e.RegistrationNumber).IsRequired();

                entity.HasOne(d => d.Airport)
                    .WithMany(p => p.Request)
                    .HasForeignKey(d => d.AirportId);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Request)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Request_Customer");

                //entity.HasOne(d => d.OfferFuelType)
                //    .WithMany(p => p.Request)
                //    .HasForeignKey(d => d.OfferFuelTypeId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_Request_OfferFuelType");

                //entity.HasOne(d => d.Offer)
                //    .WithMany(p => p.Request)
                //    .HasForeignKey(d => d.OfferId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_Request_Offer");
            });

            modelBuilder.Entity<RequestOffers>(entity =>
            {
                entity.HasIndex(e => e.OfferId);

                entity.HasIndex(e => e.RequestId);

                entity.Property(e => e.RStatus).HasColumnName("RStatus");

                //entity.Property(e => e.SStatus).HasColumnName("SStatus");

                entity.HasOne(d => d.Offer)
                    .WithMany(p => p.RequestOffers)
                    .HasForeignKey(d => d.OfferId);

                entity.HasOne(d => d.Request)
                    .WithMany(p => p.RequestOffers)
                    .HasForeignKey(d => d.RequestId);

                entity.HasOne(d => d.AirportOffer)
                    .WithMany(p => p.RequestOffers)
                    .HasForeignKey(d => d.AirportOfferId);
            });

            modelBuilder.Entity<SupplierContact>(entity =>
            {
                entity.HasIndex(e => e.ContactId);

                entity.HasIndex(e => e.FuelSupplierId);

                entity.Property(e => e.Value).IsRequired();

                entity.HasOne(d => d.Contact)
                    .WithMany(p => p.SupplierContact)
                    .HasForeignKey(d => d.ContactId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SupplierContact_Contact");

                entity.HasOne(d => d.FuelSupplier)
                    .WithMany(p => p.SupplierContact)
                    .HasForeignKey(d => d.FuelSupplierId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SupplierContact_Supplier");
            });

            modelBuilder.Entity<SupplierContactPerson>(entity =>
            {
                entity.HasIndex(e => e.FuelSupplierId);

                entity.HasOne(d => d.FuelSupplier)
                    .WithMany(p => p.SupplierContactPerson)
                    .HasForeignKey(d => d.FuelSupplierId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SupplierContactPerson_Supplier");
            });

            modelBuilder.Entity<SupplierContactPersonContact>(entity =>
            {
                entity.HasIndex(e => e.ContactId);

                entity.HasIndex(e => e.SupplierContactPersonId);

                entity.Property(e => e.Value).IsRequired();

                entity.HasOne(d => d.Contact)
                    .WithMany(p => p.SupplierContactPersonContact)
                    .HasForeignKey(d => d.ContactId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ContactPersonContacts_Contact");

                entity.HasOne(d => d.SupplierContactPerson)
                    .WithMany(p => p.SupplierContactPersonContact)
                    .HasForeignKey(d => d.SupplierContactPersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ContactPersonContacts_ContactPerson");
            });

            modelBuilder.Entity<SupplierProperties>(entity =>
            {
                entity.HasIndex(e => e.PropertyId);

                entity.HasIndex(e => e.SupplierId);

                entity.HasOne(d => d.Property)
                    .WithMany(p => p.SupplierProperties)
                    .HasForeignKey(d => d.PropertyId)
                    .HasConstraintName("FK_SupplierProperties_Property");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.SupplierProperties)
                    .HasForeignKey(d => d.SupplierId)
                    .HasConstraintName("FK_SupplierProperties_FuelSupplier");
            });

            modelBuilder.Entity<SupplierReview>(entity =>
            {
                entity.HasIndex(e => e.CustomerId);

                entity.HasIndex(e => e.FuelSupplierId);

                entity.Property(e => e.ReviewDate).HasColumnType("datetime");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.SupplierReview)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SupplierReview_Customer");

                entity.HasOne(d => d.FuelSupplier)
                    .WithMany(p => p.SupplierReview)
                    .HasForeignKey(d => d.FuelSupplierId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SupplierReview_FuelSupplier");
            });

            modelBuilder.Entity<SupplierType>(entity =>
            {
                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<ResetPasswordToken>(entity =>
            {
                entity.Property(e => e.Status).HasDefaultValue<ResetPasswordTokenStatus>(ResetPasswordTokenStatus.PendingValidation);
               
                entity.HasOne(e => e.User)
                .WithMany(e => e.ResetPasswordTokens)
                .HasForeignKey(e => e.UserId);
            });


            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            Audit();
            return base.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            Audit();
            return await base.SaveChangesAsync();
        }


        private void Audit()
        {
            var entries = ChangeTracker.Entries().Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));
            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    ((BaseEntity)entry.Entity).Created = DateTime.Now;
                    if (!((BaseEntity)entry.Entity).IsDeleted)
                        ((BaseEntity)entry.Entity).IsDeleted = false;
                    if (((BaseEntity)entry.Entity).ItemOrder <= 1)
                        ((BaseEntity)entry.Entity).ItemOrder = 1;
                }
            ((BaseEntity)entry.Entity).Modified = DateTime.Now;
            }
        }
    }
}
