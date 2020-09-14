using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FuelServices.DBContext.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdvertisementCategory",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ItemOrder = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdvertisementCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AdvertisementType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ItemOrder = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    IsValid = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdvertisementType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AdvertisementTypeProperty",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ItemOrder = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdvertisementTypeProperty", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contact",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ItemOrder = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    DisplayName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contact", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContentManagement",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ItemOrder = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    DisplayName = table.Column<string>(nullable: false),
                    IsVisible = table.Column<bool>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    AnchorText = table.Column<string>(nullable: true),
                    AnchorUrl = table.Column<string>(nullable: true),
                    ImageUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentManagement", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Continent",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ItemOrder = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Code = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Continent", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmailBodyDefaultParams",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ItemOrder = table.Column<int>(nullable: false),
                    EmailTypeName = table.Column<string>(nullable: true),
                    Title1 = table.Column<string>(nullable: true),
                    Title2 = table.Column<string>(nullable: true),
                    Title3 = table.Column<string>(nullable: true),
                    BackgroundColor = table.Column<string>(nullable: true),
                    CardColor = table.Column<string>(nullable: true),
                    FontColor = table.Column<string>(nullable: true),
                    Logo = table.Column<string>(nullable: true),
                    Banner = table.Column<string>(nullable: true),
                    ContactEmail = table.Column<string>(nullable: true),
                    CallbackUrl = table.Column<string>(nullable: true),
                    SiteUrl = table.Column<string>(nullable: true),
                    TemplateUrl = table.Column<string>(nullable: true),
                    CallbackDisplayText = table.Column<string>(nullable: true),
                    EmailCaption = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailBodyDefaultParams", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Feature",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ItemOrder = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    UnitIn = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feature", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FuelType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ItemOrder = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FuelType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Log",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(nullable: true),
                    MessageTemplate = table.Column<string>(nullable: true),
                    Level = table.Column<string>(maxLength: 128, nullable: true),
                    TimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    Exception = table.Column<string>(nullable: true),
                    Properties = table.Column<string>(type: "xml", nullable: true),
                    LogEvent = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Log", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentPackage",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ItemOrder = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    DisplayName = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Period = table.Column<int>(nullable: false),
                    ImageUrl = table.Column<string>(nullable: true),
                    Price = table.Column<double>(nullable: false),
                    PriceUnit = table.Column<string>(maxLength: 256, nullable: false),
                    Discount = table.Column<double>(nullable: true),
                    DiscountType = table.Column<int>(nullable: false),
                    DiscountUnit = table.Column<string>(maxLength: 256, nullable: true),
                    ItemLevel = table.Column<int>(nullable: false),
                    MainColor = table.Column<string>(maxLength: 256, nullable: false),
                    Type = table.Column<int>(nullable: false),
                    IsValid = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentPackage", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Property",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ItemOrder = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    ContentType = table.Column<int>(nullable: false),
                    United = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Property", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SupplierType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ItemOrder = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResetPasswordTokens",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ItemOrder = table.Column<int>(nullable: false),
                    ResetPasswordCode = table.Column<string>(nullable: true),
                    ResetPasswordCodeValidityEndDate = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false, defaultValue: 2),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResetPasswordTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResetPasswordTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ItemOrder = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    ContinentId = table.Column<int>(nullable: true),
                    ISO2 = table.Column<string>(nullable: true),
                    ISO3 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Country_Continent",
                        column: x => x.ContinentId,
                        principalTable: "Continent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "PaymentPackageFeature",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ItemOrder = table.Column<int>(nullable: false),
                    PaymentPackageId = table.Column<int>(nullable: false),
                    FeatureId = table.Column<int>(nullable: false),
                    Value = table.Column<string>(nullable: false),
                    Unit = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentPackageFeature", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentPackageFeature_PaymentPackage",
                        column: x => x.FeatureId,
                        principalTable: "Feature",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PaymentPackageFeature_Feature",
                        column: x => x.PaymentPackageId,
                        principalTable: "PaymentPackage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AdvertisementOwner",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ItemOrder = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: false),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    Phone = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Fax = table.Column<string>(nullable: true),
                    CountryId = table.Column<int>(nullable: true),
                    ImageUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdvertisementOwner", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdvertisementOwner_Country",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_AdvertisementOwner_User",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ItemOrder = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    CountryId = table.Column<int>(nullable: false),
                    State = table.Column<string>(nullable: true),
                    NameASCII = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.Id);
                    table.ForeignKey(
                        name: "FK_City_Country",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ItemOrder = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    ImageUrl = table.Column<string>(nullable: true),
                    CountryId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customer_Country",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Customer_User",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "FuelSupplier",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ItemOrder = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    CountryId = table.Column<int>(nullable: true),
                    ImageUrl = table.Column<string>(nullable: true),
                    IsMiddler = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FuelSupplier", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Supplier_Country",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Supplier_AspNetUsers",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Advertisement",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ItemOrder = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    AnchorUrl = table.Column<string>(nullable: true),
                    Caption = table.Column<string>(nullable: true),
                    ImageUrl = table.Column<string>(nullable: false),
                    CaptionClicks = table.Column<int>(nullable: false),
                    AdvertisementOwnerId = table.Column<int>(nullable: true),
                    AdvertisementCategoryId = table.Column<int>(nullable: true),
                    AdvertisementTypeId = table.Column<int>(nullable: false),
                    Price = table.Column<float>(nullable: false),
                    PublishDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Advertisement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Advertisement_AdvertisementCategory",
                        column: x => x.AdvertisementCategoryId,
                        principalTable: "AdvertisementCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Advertisment_AdvertismentOwner",
                        column: x => x.AdvertisementOwnerId,
                        principalTable: "AdvertisementOwner",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Advertisment_AdvertismentType",
                        column: x => x.AdvertisementTypeId,
                        principalTable: "AdvertisementType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Airport",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ItemOrder = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    CityId = table.Column<int>(nullable: true),
                    NearestCityId = table.Column<int>(nullable: true),
                    CountryId = table.Column<int>(nullable: true),
                    ICAO = table.Column<string>(nullable: true),
                    IATA = table.Column<string>(nullable: true),
                    Lat = table.Column<float>(nullable: true),
                    Long = table.Column<float>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    Variation = table.Column<float>(nullable: true),
                    Elevation = table.Column<float>(nullable: true),
                    Views = table.Column<int>(nullable: true),
                    Continent = table.Column<string>(nullable: true),
                    IsoCountry = table.Column<string>(nullable: true),
                    IsoRegion = table.Column<string>(nullable: true),
                    Municipality = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Airport", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Airport_City",
                        column: x => x.CityId,
                        principalTable: "City",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Airport_Country",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Airport_City1",
                        column: x => x.NearestCityId,
                        principalTable: "City",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContactUs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ItemOrder = table.Column<int>(nullable: false),
                    CustomerId = table.Column<int>(nullable: true),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Tel = table.Column<string>(nullable: false),
                    Subject = table.Column<string>(nullable: false),
                    Message = table.Column<string>(nullable: false),
                    SubmitDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    IsRead = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactUs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContactUs_Customer",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "CustomerPackagesLog",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ItemOrder = table.Column<int>(nullable: false),
                    CustomerId = table.Column<int>(nullable: true),
                    PaymentPackageId = table.Column<int>(nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Company = table.Column<string>(nullable: true),
                    CountryId = table.Column<int>(nullable: false),
                    CityId = table.Column<int>(nullable: false),
                    PostalCode = table.Column<string>(nullable: false),
                    Address1 = table.Column<string>(nullable: false),
                    Address2 = table.Column<string>(nullable: true),
                    Phone1 = table.Column<string>(nullable: false),
                    Phone2 = table.Column<string>(nullable: true),
                    Fax = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerPackagesLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerPackagesLog_City_CityId",
                        column: x => x.CityId,
                        principalTable: "City",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerPackagesLog_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerPackagesLog_Customer",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_CustomerPackagesLog_PaymentPackage",
                        column: x => x.PaymentPackageId,
                        principalTable: "PaymentPackage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Offer",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ItemOrder = table.Column<int>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    FuelSupplierId = table.Column<int>(nullable: false),
                    Status = table.Column<string>(nullable: false),
                    DuesTaxesLevies = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Offer_FuelSupplier_FuelSupplierId",
                        column: x => x.FuelSupplierId,
                        principalTable: "FuelSupplier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SupplierContact",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ItemOrder = table.Column<int>(nullable: false),
                    FuelSupplierId = table.Column<int>(nullable: false),
                    ContactId = table.Column<int>(nullable: false),
                    Value = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierContact", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SupplierContact_Contact",
                        column: x => x.ContactId,
                        principalTable: "Contact",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SupplierContact_Supplier",
                        column: x => x.FuelSupplierId,
                        principalTable: "FuelSupplier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SupplierContactPerson",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ItemOrder = table.Column<int>(nullable: false),
                    FuelSupplierId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    JobTitle = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierContactPerson", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SupplierContactPerson_Supplier",
                        column: x => x.FuelSupplierId,
                        principalTable: "FuelSupplier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SupplierPackagesLog",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ItemOrder = table.Column<int>(nullable: false),
                    FuelSupplierId = table.Column<int>(nullable: true),
                    PaymentPackageId = table.Column<int>(nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Company = table.Column<string>(nullable: true),
                    CountryId = table.Column<int>(nullable: false),
                    CityId = table.Column<int>(nullable: false),
                    PostalCode = table.Column<string>(nullable: false),
                    Address1 = table.Column<string>(nullable: false),
                    Address2 = table.Column<string>(nullable: true),
                    Phone1 = table.Column<string>(nullable: false),
                    Phone2 = table.Column<string>(nullable: true),
                    Fax = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierPackagesLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SupplierPackagesLog_City_CityId",
                        column: x => x.CityId,
                        principalTable: "City",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SupplierPackagesLog_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FuelSupplierPackagesLog_Customer",
                        column: x => x.FuelSupplierId,
                        principalTable: "FuelSupplier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_FuelSupplierPackagesLog_PaymentPackage",
                        column: x => x.PaymentPackageId,
                        principalTable: "PaymentPackage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "SupplierProperties",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ItemOrder = table.Column<int>(nullable: false),
                    PropertyId = table.Column<int>(nullable: false),
                    SupplierId = table.Column<int>(nullable: false),
                    Value = table.Column<string>(nullable: true),
                    Unit = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierProperties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SupplierProperties_Property",
                        column: x => x.PropertyId,
                        principalTable: "Property",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SupplierProperties_FuelSupplier",
                        column: x => x.SupplierId,
                        principalTable: "FuelSupplier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SupplierReview",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ItemOrder = table.Column<int>(nullable: false),
                    Rate = table.Column<int>(nullable: false),
                    Value = table.Column<string>(nullable: true),
                    FuelSupplierId = table.Column<int>(nullable: false),
                    CustomerId = table.Column<int>(nullable: false),
                    ReviewDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierReview", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SupplierReview_Customer",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SupplierReview_FuelSupplier",
                        column: x => x.FuelSupplierId,
                        principalTable: "FuelSupplier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AdvertisementProperty",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ItemOrder = table.Column<int>(nullable: false),
                    AdvertisementId = table.Column<int>(nullable: false),
                    AdvertisementTypePropertyId = table.Column<int>(nullable: false),
                    Value = table.Column<string>(nullable: false),
                    Unit = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdvertisementProperty", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdvertismentProperties_Advertisment",
                        column: x => x.AdvertisementId,
                        principalTable: "Advertisement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AdvertismentProperties_AdvertismentTypeProperty",
                        column: x => x.AdvertisementTypePropertyId,
                        principalTable: "AdvertisementTypeProperty",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AirportProperty",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ItemOrder = table.Column<int>(nullable: false),
                    AirportId = table.Column<int>(nullable: false),
                    PropertyId = table.Column<int>(nullable: false),
                    Value = table.Column<string>(nullable: false),
                    Unit = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AirportProperty", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AirportPorperties_Airports",
                        column: x => x.AirportId,
                        principalTable: "Airport",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AirportPorperties_Property",
                        column: x => x.PropertyId,
                        principalTable: "Property",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Request",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ItemOrder = table.Column<int>(nullable: false),
                    CustomerId = table.Column<int>(nullable: false),
                    AirportId = table.Column<int>(nullable: false),
                    NameOfOperator = table.Column<string>(nullable: true),
                    CaptainFmName = table.Column<string>(nullable: true),
                    SendDate = table.Column<DateTime>(nullable: false),
                    ArrivalDate = table.Column<DateTime>(nullable: false, defaultValueSql: "('0001-01-01T00:00:00.0000000')"),
                    DepartureDate = table.Column<DateTime>(nullable: false, defaultValueSql: "('0001-01-01T00:00:00.0000000')"),
                    RegistrationNumber = table.Column<string>(nullable: false),
                    AircraftType = table.Column<string>(nullable: false),
                    CallSign = table.Column<string>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    Notes = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Request", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Request_Airport_AirportId",
                        column: x => x.AirportId,
                        principalTable: "Airport",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Request_Customer",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AirportOffer",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ItemOrder = table.Column<int>(nullable: false),
                    OfferId = table.Column<int>(nullable: false),
                    AirportId = table.Column<int>(nullable: true),
                    CityId = table.Column<int>(nullable: true),
                    Price = table.Column<double>(nullable: false),
                    PriceUnit = table.Column<string>(nullable: false),
                    SupplierTypeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AirportOffer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AirportOffer_Airport_AirportId",
                        column: x => x.AirportId,
                        principalTable: "Airport",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AirportOffer_City_CityId",
                        column: x => x.CityId,
                        principalTable: "City",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AirportOffer_Offer_OfferId",
                        column: x => x.OfferId,
                        principalTable: "Offer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AirportOffer_SupplierType_SupplierTypeId",
                        column: x => x.SupplierTypeId,
                        principalTable: "SupplierType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OfferFuelType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ItemOrder = table.Column<int>(nullable: false),
                    FuelTypeId = table.Column<int>(nullable: false),
                    OfferId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfferFuelType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OfferFuelType_FuelType_FuelTypeId",
                        column: x => x.FuelTypeId,
                        principalTable: "FuelType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OfferFuelType_Offer_OfferId",
                        column: x => x.OfferId,
                        principalTable: "Offer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OfferProperties",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ItemOrder = table.Column<int>(nullable: false),
                    PropertyId = table.Column<int>(nullable: false),
                    OfferId = table.Column<int>(nullable: false),
                    Value = table.Column<string>(nullable: true),
                    Unit = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfferProperties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OfferProperties_Offer",
                        column: x => x.OfferId,
                        principalTable: "Offer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OfferProperties_Property",
                        column: x => x.PropertyId,
                        principalTable: "Property",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SupplierContactPersonContact",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ItemOrder = table.Column<int>(nullable: false),
                    ContactId = table.Column<int>(nullable: false),
                    SupplierContactPersonId = table.Column<int>(nullable: false),
                    Value = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierContactPersonContact", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContactPersonContacts_Contact",
                        column: x => x.ContactId,
                        principalTable: "Contact",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContactPersonContacts_ContactPerson",
                        column: x => x.SupplierContactPersonId,
                        principalTable: "SupplierContactPerson",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OfferPartyExcludes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ItemOrder = table.Column<int>(nullable: false),
                    AirportOfferId = table.Column<int>(nullable: false),
                    AirportId = table.Column<int>(nullable: true),
                    CityId = table.Column<int>(nullable: true),
                    CountryId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfferPartyExcludes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OfferPartyExcludes_Airport_AirportId",
                        column: x => x.AirportId,
                        principalTable: "Airport",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OfferPartyExcludes_AirportOffer_AirportOfferId",
                        column: x => x.AirportOfferId,
                        principalTable: "AirportOffer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OfferPartyExcludes_City_CityId",
                        column: x => x.CityId,
                        principalTable: "City",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OfferPartyExcludes_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RequestOffers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ItemOrder = table.Column<int>(nullable: false),
                    RequestId = table.Column<int>(nullable: false),
                    OfferId = table.Column<int>(nullable: false),
                    AirportOfferId = table.Column<int>(nullable: false),
                    RStatus = table.Column<int>(nullable: false),
                    SupplierConfirmDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestOffers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestOffers_AirportOffer_AirportOfferId",
                        column: x => x.AirportOfferId,
                        principalTable: "AirportOffer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RequestOffers_Offer_OfferId",
                        column: x => x.OfferId,
                        principalTable: "Offer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RequestOffers_Request_RequestId",
                        column: x => x.RequestId,
                        principalTable: "Request",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Advertisement_AdvertisementCategoryId",
                table: "Advertisement",
                column: "AdvertisementCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Advertisement_AdvertisementOwnerId",
                table: "Advertisement",
                column: "AdvertisementOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Advertisement_AdvertisementTypeId",
                table: "Advertisement",
                column: "AdvertisementTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AdvertisementOwner_CountryId",
                table: "AdvertisementOwner",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_AdvertisementOwner_UserId",
                table: "AdvertisementOwner",
                column: "UserId",
                unique: true,
                filter: "([UserId] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "IX_AdvertisementProperty_AdvertisementId",
                table: "AdvertisementProperty",
                column: "AdvertisementId");

            migrationBuilder.CreateIndex(
                name: "IX_AdvertisementProperty_AdvertisementTypePropertyId",
                table: "AdvertisementProperty",
                column: "AdvertisementTypePropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_Airport_CityId",
                table: "Airport",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Airport_CountryId",
                table: "Airport",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Airport_NearestCityId",
                table: "Airport",
                column: "NearestCityId");

            migrationBuilder.CreateIndex(
                name: "IX_AirportOffer_AirportId",
                table: "AirportOffer",
                column: "AirportId");

            migrationBuilder.CreateIndex(
                name: "IX_AirportOffer_CityId",
                table: "AirportOffer",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_AirportOffer_OfferId",
                table: "AirportOffer",
                column: "OfferId");

            migrationBuilder.CreateIndex(
                name: "IX_AirportOffer_SupplierTypeId",
                table: "AirportOffer",
                column: "SupplierTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AirportProperty_AirportId",
                table: "AirportProperty",
                column: "AirportId");

            migrationBuilder.CreateIndex(
                name: "IX_AirportProperty_PropertyId",
                table: "AirportProperty",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_City_CountryId",
                table: "City",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactUs_CustomerId",
                table: "ContactUs",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Country_ContinentId",
                table: "Country",
                column: "ContinentId");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_CountryId",
                table: "Customer",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_UserId",
                table: "Customer",
                column: "UserId",
                unique: true,
                filter: "([UserId] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerPackagesLog_CityId",
                table: "CustomerPackagesLog",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerPackagesLog_CountryId",
                table: "CustomerPackagesLog",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerPackagesLog_CustomerId",
                table: "CustomerPackagesLog",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerPackagesLog_PaymentPackageId",
                table: "CustomerPackagesLog",
                column: "PaymentPackageId");

            migrationBuilder.CreateIndex(
                name: "IX_FuelSupplier_CountryId",
                table: "FuelSupplier",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_FuelSupplier_UserId",
                table: "FuelSupplier",
                column: "UserId",
                unique: true,
                filter: "([UserId] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "IX_Offer_FuelSupplierId",
                table: "Offer",
                column: "FuelSupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_OfferFuelType_FuelTypeId",
                table: "OfferFuelType",
                column: "FuelTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_OfferFuelType_OfferId",
                table: "OfferFuelType",
                column: "OfferId");

            migrationBuilder.CreateIndex(
                name: "IX_OfferPartyExcludes_AirportId",
                table: "OfferPartyExcludes",
                column: "AirportId");

            migrationBuilder.CreateIndex(
                name: "IX_OfferPartyExcludes_AirportOfferId",
                table: "OfferPartyExcludes",
                column: "AirportOfferId");

            migrationBuilder.CreateIndex(
                name: "IX_OfferPartyExcludes_CityId",
                table: "OfferPartyExcludes",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_OfferPartyExcludes_CountryId",
                table: "OfferPartyExcludes",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_OfferProperties_OfferId",
                table: "OfferProperties",
                column: "OfferId");

            migrationBuilder.CreateIndex(
                name: "IX_OfferProperties_PropertyId",
                table: "OfferProperties",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentPackageFeature_FeatureId",
                table: "PaymentPackageFeature",
                column: "FeatureId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentPackageFeature_PaymentPackageId",
                table: "PaymentPackageFeature",
                column: "PaymentPackageId");

            migrationBuilder.CreateIndex(
                name: "IX_Request_AirportId",
                table: "Request",
                column: "AirportId");

            migrationBuilder.CreateIndex(
                name: "IX_Request_CustomerId",
                table: "Request",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestOffers_AirportOfferId",
                table: "RequestOffers",
                column: "AirportOfferId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestOffers_OfferId",
                table: "RequestOffers",
                column: "OfferId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestOffers_RequestId",
                table: "RequestOffers",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_ResetPasswordTokens_UserId",
                table: "ResetPasswordTokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierContact_ContactId",
                table: "SupplierContact",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierContact_FuelSupplierId",
                table: "SupplierContact",
                column: "FuelSupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierContactPerson_FuelSupplierId",
                table: "SupplierContactPerson",
                column: "FuelSupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierContactPersonContact_ContactId",
                table: "SupplierContactPersonContact",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierContactPersonContact_SupplierContactPersonId",
                table: "SupplierContactPersonContact",
                column: "SupplierContactPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierPackagesLog_CityId",
                table: "SupplierPackagesLog",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierPackagesLog_CountryId",
                table: "SupplierPackagesLog",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierPackagesLog_FuelSupplierId",
                table: "SupplierPackagesLog",
                column: "FuelSupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierPackagesLog_PaymentPackageId",
                table: "SupplierPackagesLog",
                column: "PaymentPackageId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierProperties_PropertyId",
                table: "SupplierProperties",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierProperties_SupplierId",
                table: "SupplierProperties",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierReview_CustomerId",
                table: "SupplierReview",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierReview_FuelSupplierId",
                table: "SupplierReview",
                column: "FuelSupplierId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdvertisementProperty");

            migrationBuilder.DropTable(
                name: "AirportProperty");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "ContactUs");

            migrationBuilder.DropTable(
                name: "ContentManagement");

            migrationBuilder.DropTable(
                name: "CustomerPackagesLog");

            migrationBuilder.DropTable(
                name: "EmailBodyDefaultParams");

            migrationBuilder.DropTable(
                name: "Log");

            migrationBuilder.DropTable(
                name: "OfferFuelType");

            migrationBuilder.DropTable(
                name: "OfferPartyExcludes");

            migrationBuilder.DropTable(
                name: "OfferProperties");

            migrationBuilder.DropTable(
                name: "PaymentPackageFeature");

            migrationBuilder.DropTable(
                name: "RequestOffers");

            migrationBuilder.DropTable(
                name: "ResetPasswordTokens");

            migrationBuilder.DropTable(
                name: "SupplierContact");

            migrationBuilder.DropTable(
                name: "SupplierContactPersonContact");

            migrationBuilder.DropTable(
                name: "SupplierPackagesLog");

            migrationBuilder.DropTable(
                name: "SupplierProperties");

            migrationBuilder.DropTable(
                name: "SupplierReview");

            migrationBuilder.DropTable(
                name: "Advertisement");

            migrationBuilder.DropTable(
                name: "AdvertisementTypeProperty");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "FuelType");

            migrationBuilder.DropTable(
                name: "Feature");

            migrationBuilder.DropTable(
                name: "AirportOffer");

            migrationBuilder.DropTable(
                name: "Request");

            migrationBuilder.DropTable(
                name: "Contact");

            migrationBuilder.DropTable(
                name: "SupplierContactPerson");

            migrationBuilder.DropTable(
                name: "PaymentPackage");

            migrationBuilder.DropTable(
                name: "Property");

            migrationBuilder.DropTable(
                name: "AdvertisementCategory");

            migrationBuilder.DropTable(
                name: "AdvertisementOwner");

            migrationBuilder.DropTable(
                name: "AdvertisementType");

            migrationBuilder.DropTable(
                name: "Offer");

            migrationBuilder.DropTable(
                name: "SupplierType");

            migrationBuilder.DropTable(
                name: "Airport");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "FuelSupplier");

            migrationBuilder.DropTable(
                name: "City");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Country");

            migrationBuilder.DropTable(
                name: "Continent");
        }
    }
}
